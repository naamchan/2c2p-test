CREATE TABLE `2c2p_test`.`transactions` (
    `id` VARCHAR(50) NOT NULL , 
    `amount` DECIMAL NOT NULL , 
    `currency_code` CHAR(3) NOT NULL , 
    `transaction_date` DATETIME NOT NULL , 
    `status` TINYINT NOT NULL , 
    `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP , 
    PRIMARY KEY (`id`(50))
) ENGINE = InnoDB;

ALTER TABLE `2c2p_test`.`transactions` ADD INDEX `IDX_currency_code` (`currency_code`);
ALTER TABLE `2c2p_test`.`transactions` ADD INDEX `IDX_status` (`status`);
ALTER TABLE `2c2p_test`.`transactions` ADD INDEX `IDX_transaction_date` (`transaction_date`);