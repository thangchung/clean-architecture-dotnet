extern crate openssl;
#[macro_use]
extern crate diesel_migrations;

use actix_web::{get, middleware, web, App, Error, HttpResponse, HttpServer, Responder};
use listenfd::ListenFd;
use std::env;

use ::audit_service_lib::db_connection::establish_connection;
use ::audit_service_lib::db_connection::PostgresDb;
use ::audit_service_lib::domain::history;

embed_migrations!();

#[get("/")]
async fn index() -> impl Responder {
    HttpResponse::Ok().body("Hello from audit-service!")
}

#[get("/histories")]
async fn get_histories(pool: web::Data<PostgresDb>) -> Result<HttpResponse, Error> {
    let conn = pool.get().expect("Cannot get db connection");
    match history::History::get_histories(&conn) {
      Ok(data) => Ok(HttpResponse::Ok().json(data)),
      Err(error) => Err(actix_web::error::ErrorInternalServerError(error))
    }
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    openssl_probe::init_ssl_cert_env_vars();

    std::env::set_var("RUST_LOG", "actix_web=info");
    std::env::set_var("RUST_BACKTRACE", "full");

    env_logger::init();
    dotenv::dotenv().ok();

    let mut listenfd = ListenFd::from_env();

    let pool = establish_connection();

    println!("Running migration...");
    let migration_pool_conn = pool.clone().get().expect("Cannot get db connection");
    embedded_migrations::run(&migration_pool_conn);

    let mut server = HttpServer::new(move || {
        App::new()
            .data(pool.clone())
            .wrap(middleware::Logger::default())
            .service(index)
            .service(get_histories)
    });

    server = match listenfd.take_tcp_listener(0)? {
        Some(listener) => server.listen(listener)?,
        None => {
            let host = env::var("HOST").expect("HOST is not set in .env file");
            let port = env::var("PORT").expect("PORT is not set in .env file");
            server.bind(format!("{}:{}", host, port))?
        }
    };

    println!("Starting server");
    server.run().await?;

    Ok(())
}
