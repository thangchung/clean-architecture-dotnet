#[warn(unused_must_use)]
#[macro_use]
pub extern crate diesel;
pub extern crate serde;
pub extern crate serde_derive;
pub extern crate serde_json;

pub mod application;
pub mod db_connection;
pub mod domain;
pub mod schema;
