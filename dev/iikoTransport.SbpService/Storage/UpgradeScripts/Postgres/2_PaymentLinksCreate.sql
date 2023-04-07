create table if not exists payment_links
(
    id uuid not null,
    qrc_id varchar not null,
    paramsId varchar null,
    terminal_group_uoc_id uuid not null,
    terminal_id uuid null,
    updated_at timestamp not null,
    constraint payment_links_pkey primary key (id)
);