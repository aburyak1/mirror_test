CREATE TABLE IF NOT EXISTS sbp_settings
(
    id uuid NOT NULL,
    terminal_group_uoc_id uuid NOT NULL,
    merchantId varchar NOT NULL,
    account varchar NOT NULL,
    memberId varchar NOT NULL,
    CONSTRAINT sbp_settings_pkey PRIMARY KEY (id)
);

create index sbp_settings_tg_index
	on sbp_settings (terminal_group_uoc_id);