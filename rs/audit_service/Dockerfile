ARG BASE_IMAGE=ekidd/rust-musl-builder:latest

FROM ${BASE_IMAGE} AS base

ENV USER=rust
WORKDIR /code
RUN cargo init
COPY --chown=rust:rust Cargo.toml /code/Cargo.toml
RUN cargo fetch

COPY src /code/src
COPY migrations /code/migrations

FROM base AS builder

RUN cargo build --release

FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY --from=builder \
    /code/target/x86_64-unknown-linux-musl/release/audit_service \
    /usr/local/bin/

EXPOSE 5010

ENTRYPOINT [ "/usr/local/bin/audit_service" ]
