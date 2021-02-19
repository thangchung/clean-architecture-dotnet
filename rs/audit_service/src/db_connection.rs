use diesel::pg::PgConnection;
//use diesel::prelude::*;
use diesel::r2d2::{ConnectionManager, Pool};
use dotenv::dotenv;
use std::env;

pub type PostgresDb = Pool<ConnectionManager<PgConnection>>;

pub fn establish_connection() -> PostgresDb {
    dotenv().ok(); // load env variables

    let database_url = env::var("DATABASE_URL").expect("DATABASE_URL must be set");

    let manager = ConnectionManager::<PgConnection>::new(database_url);
    Pool::builder()
        .max_size(10)
        .build(manager)
        .expect("Failed  to create pool")
    //PgConnection::establish(&database_url).expect(&format!("Failed to connect to {}", database_url))
}
