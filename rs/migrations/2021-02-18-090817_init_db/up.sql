-- Your SQL goes here

CREATE TABLE histories (
  id SERIAL PRIMARY KEY,
  entity_type VARCHAR NOT NULL,
  metadata TEXT NOT NULL,
  created DATE NOT NULL DEFAULT now(),
  correlation_id VARCHAR NOT NULL
)
