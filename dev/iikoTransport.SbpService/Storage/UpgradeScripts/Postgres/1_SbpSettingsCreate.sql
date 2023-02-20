create table if not exists sbp_settings
(
    id uuid not null,
    terminal_group_uoc_id uuid not null,
    merchant_id varchar not null,
    account varchar not null,
    member_id varchar not null,
    constraint sbp_settings_pkey primary key (id)
);

create index if not exists sbp_settings_tg_index
	on sbp_settings (terminal_group_uoc_id);