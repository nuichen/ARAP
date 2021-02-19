--数据库升级

--2019-11-30 17:53
CREATE TABLE pda_ot_t_check_bak
(
    sheet_no VARCHAR(25) NOT NULL,
    branch_no VARCHAR(20) NOT NULL,
    item_no VARCHAR(20) NOT NULL,
    stock_qty DECIMAL(18, 4) NULL
        DEFAULT 0,
    cost_price DECIMAL(18, 4) NULL
        DEFAULT 0,
    price DECIMAL(18, 4) NULL
        DEFAULT 0,
    sale_price DECIMAL(18, 4) NULL
        DEFAULT 0,
    PRIMARY KEY
    (
        sheet_no,
        branch_no,
        item_no
    )
);

CREATE TABLE pda_ot_t_check_detail
(
    sheet_no VARCHAR(25)  NOT NULL,
    flow_id INT NOT NULL,
    counter_no VARCHAR(50) NULL,
    item_no VARCHAR(25) NOT NULL,
    input_code VARCHAR(50) NULL,
    qty DECIMAL(18, 4) NULL
        DEFAULT 0,
    jh DECIMAL(18, 4) NULL
        DEFAULT 0,
    oper_time DATETIME NOT NULL,
    oper_man VARCHAR(25) NULL,
    oper_type VARCHAR(20) NULL,
    master_no VARCHAR(20) NULL,
	PRIMARY KEY(sheet_no,flow_id,item_no)
);

CREATE TABLE pda_st_t_oper_info
(
    oper_id VARCHAR(20) PRIMARY KEY NOT NULL,
    oper_name VARCHAR(50) NULL,
    pwd VARCHAR(50) NULL,
    oper_type VARCHAR(1) NULL,
    status VARCHAR(1) NULL,
    create_time DATETIME NULL
);

CREATE TABLE pda_bi_t_item_info
(
    item_no VARCHAR(20) PRIMARY KEY NOT NULL,
    item_subno VARCHAR(25) NOT NULL,
    item_name VARCHAR(50) NULL,
    unit_no VARCHAR(20) NULL,
    item_size VARCHAR(20) NULL,
    barcode VARCHAR(20) NULL,
    price DECIMAL(18, 4) NULL,
    sale_price DECIMAL(18, 4) NULL,
    item_subname VARCHAR(50) NULL,
    item_flag VARCHAR(2) NULL,
    combine_sta VARCHAR(2) NULL
);

CREATE TABLE pda_bi_t_item_barcode
(
    item_no VARCHAR(20) NOT NULL,
    barcode VARCHAR(50) NULL
);

CREATE TABLE pda_bi_t_item_pack_detail
(
    master_no VARCHAR(25) NOT NULL,
    item_no VARCHAR(20) NOT NULL,
    pack_num DECIMAL(18, 4) NULL
);


INSERT dbo.sys_t_system
(
    sys_var_id,
    sys_var_name,
    sys_var_value,
    is_changed,
    sys_var_desc,
    sys_ver_flag,
    update_time
)
VALUES
(   'branch_no', -- sys_var_id - varchar(20)
    '机构号',       -- sys_var_name - varchar(40)
    '',          -- sys_var_value - varchar(250)
    '1',         -- is_changed - varchar(2)
    '机构号',       -- sys_var_desc - varchar(100)
    '1',         -- sys_ver_flag - varchar(1)
    GETDATE()    -- update_time - datetime
    );


INSERT dbo.sys_t_system
(
    sys_var_id,
    sys_var_name,
    sys_var_value,
    is_changed,
    sys_var_desc,
    sys_ver_flag,
    update_time
)
VALUES
(   'check_back_sheet', -- sys_var_id - varchar(20)
    '盘点批次号',            -- sys_var_name - varchar(40)
    '',                 -- sys_var_value - varchar(250)
    '1',                -- is_changed - varchar(2)
    '盘点批次号',            -- sys_var_desc - varchar(100)
    '1',                -- sys_ver_flag - varchar(1)
    GETDATE()           -- update_time - datetime
    );
