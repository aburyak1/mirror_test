create table if not exists payment_links
(
    qrc_id varchar not null,
    terminal_group_uoc_id uuid not null,
    updated_at timestamp not null,
    constraint payment_links_pkey primary key (qrc_id)
);