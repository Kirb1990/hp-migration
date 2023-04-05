ALTER TABLE `customers`
    ADD COLUMN `bluuubb` VARCHAR(255) NULL AFTER `last_name`;
UPDATE customers SET last_name = 'test';