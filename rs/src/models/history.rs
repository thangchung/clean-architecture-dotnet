use chrono::{DateTime, Utc};
use crate::schema::histories;

#[derive(Queryable)]
pub struct History {
  pub id: i32,
  pub entity_type: String,
  pub metadata: String,
  pub created: DateTime<Utc>
}
