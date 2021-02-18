table! {
    histories (id) {
        id -> Int4,
        entity_type -> Varchar,
        metadata -> Text,
        created -> Date,
        correlation_id -> Varchar,
    }
}
