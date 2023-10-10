-- DROP INDEX "user_unique_email" ------------------------------
DROP INDEX `user_unique_email` ON `user`;
-- -------------------------------------------------------------

-- CHANGE "UNIQUE" OF "FIELD "email" ---------------------------
-- Will be changed by indexes
-- -------------------------------------------------------------

-- CREATE FIELD "first_name" -----------------------------------
ALTER TABLE `user` ADD COLUMN `first_name` VarChar( 50 ) NOT NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "last_name" ------------------------------------
ALTER TABLE `user` ADD COLUMN `last_name` VarChar( 50 ) NOT NULL;
-- -------------------------------------------------------------

-- CHANGE "NAME" OF "FIELD "password_hash" ---------------------
ALTER TABLE `user` CHANGE `login_password` `password_hash` VarChar( 255 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL;
-- -------------------------------------------------------------

-- CREATE VIEW "user_view" -------------------------------------
CREATE OR REPLACE VIEW `user_view` AS (
    SELECT `id`, `password_hash`, `email`, `is_verified`
    FROM `user`
    WHERE `is_deleted` = 0
);
-- -------------------------------------------------------------
