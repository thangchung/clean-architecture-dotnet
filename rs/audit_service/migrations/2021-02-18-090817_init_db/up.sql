-- Your SQL goes here

CREATE SCHEMA audit;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE audit.histories (
  id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
  entity_type VARCHAR NOT NULL,
  metadata TEXT NOT NULL,
  created TIMESTAMP NOT NULL DEFAULT now(),
  correlation_id VARCHAR NOT NULL
)
