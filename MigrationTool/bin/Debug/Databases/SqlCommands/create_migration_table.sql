CREATE TABLE IF NOT EXISTS `migrations` (
    `id` INT(11) NOT NULL AUTO_INCREMENT,
    `migration` VARCHAR(255) NOT NULL,
    `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `migration_name_UNIQUE` (`migration`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
