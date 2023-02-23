create table if not exists refund_requests
(
    opkc_refund_request_id varchar not null,
    trx_id varchar not null,
    terminal_group_uoc_id uuid not null,
    constraint refund_requests_pkey primary key (opkc_refund_request_id)
);