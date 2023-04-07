create table if not exists refund_requests
(
    id uuid not null,
    opkc_refund_request_id varchar not null,
    trx_id varchar not null,
    terminal_group_uoc_id uuid not null,
    terminal_id uuid null,
    updated_at timestamp not null,
    constraint refund_requests_pkey primary key (id)
);