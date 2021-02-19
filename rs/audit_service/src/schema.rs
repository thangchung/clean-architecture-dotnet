table! {
    audit.histories (id) {
        id -> Uuid,
        entity_type -> Varchar,
        metadata -> Text,
        created -> Timestamp,
        correlation_id -> Varchar,
    }
}
