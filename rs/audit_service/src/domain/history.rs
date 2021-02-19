use diesel::prelude::*;
use chrono::NaiveDateTime;
use serde::{Deserialize, Serialize};

use crate::schema::histories;
use crate::schema::histories::dsl::*;

#[derive(Debug, Queryable, Identifiable, Serialize, Deserialize)]
#[table_name = "histories"]
pub struct History {
    pub id: uuid::Uuid,
    pub entity_type: String,
    pub metadata: String,
    pub created: NaiveDateTime,
    pub correlation_id: String,
}

impl History {
  pub fn get_histories(connection: &PgConnection) -> Result<Vec<Self>, diesel::result::Error>
  {
    Ok(histories.load::<History>(connection)?)
  }
}
