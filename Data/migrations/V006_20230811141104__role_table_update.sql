-- Target Database ---------------------------------------------
USE `docconnect`;
-- -------------------------------------------------------------

-- DROP CONSTRAINT `user_email_validation` ---------------------
ALTER TABLE `user` DROP CONSTRAINT `user_email_validation`;
-- -------------------------------------------------------------

-- CHANGE "NULLABLE" OF "FIELD "normalized_name" ---------------
ALTER TABLE `role` MODIFY `normalized_name` VarChar( 255 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL;
-- -------------------------------------------------------------

-- CHANGE "NULLABLE" OF "FIELD "concurrency_stamp" -------------
ALTER TABLE `role` MODIFY `concurrency_stamp` LongText CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL;
-- -------------------------------------------------------------

