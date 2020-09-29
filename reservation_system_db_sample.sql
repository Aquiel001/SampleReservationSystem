# SQL Manager 2010 for MySQL 4.5.0.9
# ---------------------------------------
# Host     : localhost
# Port     : 3306
# Database : reservation_system_db_sample


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES latin1 */;

SET FOREIGN_KEY_CHECKS=0;

DROP DATABASE IF EXISTS `reservation_system_db_sample`;

CREATE DATABASE `reservation_system_db_sample`
    CHARACTER SET 'latin1'
    COLLATE 'latin1_swedish_ci';

USE `reservation_system_db_sample`;

SET sql_mode = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';

#
# Structure for the `usertypes` table : 
#

DROP TABLE IF EXISTS `usertypes`;

CREATE TABLE `usertypes` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `Description` varchar(20) DEFAULT 'Default',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Structure for the `users` table : 
#

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `Id` varchar(36) NOT NULL,
  `ContactName` varchar(20) NOT NULL,
  `ContactType` int(10) NOT NULL,
  `PhoneNumber` int(13) NOT NULL,
  `BirthDate` varchar(10) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  UNIQUE KEY `ContactName` (`ContactName`),
  KEY `ContactType` (`ContactType`),
  CONSTRAINT `users_fk` FOREIGN KEY (`ContactType`) REFERENCES `usertypes` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Structure for the `reservations` table : 
#

DROP TABLE IF EXISTS `reservations`;

CREATE TABLE `reservations` (
  `Id` varchar(36) NOT NULL,
  `UserId` varchar(36) NOT NULL,
  `Details` varchar(1000) DEFAULT NULL,
  `Ranking` int(10) DEFAULT '3',
  `IsFavorite` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `reservations_fk` (`UserId`),
  CONSTRAINT `reservations_fk` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Definition for the `add_new_user` procedure : 
#

DELIMITER $$

DROP PROCEDURE IF EXISTS `add_new_user`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `add_new_user`(
        IN contact_name VARCHAR(20),
        IN contact_type INTEGER(11),
        IN phone_number VARCHAR(12),
        IN birth_date VARCHAR(16),
        IN userId VARCHAR(36),
        IN details VARCHAR(250),
        IN reservationId VARCHAR(36),
        IN isFavorite BOOLEAN,
        IN ranking INTEGER(11)
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN	
	IF ( exists(select `id` from `users` WHERE `users`.`ContactName` = contact_name))
	THEN
    START TRANSACTION;
		UPDATE 
  		`users`  
		SET 
  		`users`.`ContactType` = contact_type,
  		`users`.`PhoneNumber` = phone_number,
  		`users`.`BirthDate` = birth_date
 
		WHERE 
  		`ContactName` = contact_name;
        Commit;
        IF ( exists(select `id` from `reservations` WHERE `reservations`.`Id` = reservationId))
        THEN
         START TRANSACTION;
		UPDATE 
  		`reservations`  
		SET 
  		`reservations`.`Details`= details,
  		`reservations`.`IsFavorite` = isFavorite,
  		`reservations`.`Ranking` = ranking
		WHERE 
  		`reservations`.`Id` = reservationId;
        Commit;
        ELSE
         START TRANSACTION;
		INSERT INTO 
  		`reservations`
		(
  		`reservations`.`Id`,
  		`reservations`.`Details`,
  		`reservations`.`IsFavorite`,
  		`reservations`.`Ranking`,
  		`reservations`.`UserId`
		) 
		VALUE (
  		reservationId,
 	 	details,
  		isFavorite,
  		ranking,
 		 userId
         );
        COMMIT;
        END IF;
        
	ELSE
    START TRANSACTION;
		INSERT INTO 
  		`users`
		(
  		`users`.`Id`,
  		`users`.`ContactName`,
  		`users`.`ContactType`,
  		`users`.`PhoneNumber`,
  		`users`.`BirthDate`
		) 
		VALUE (
  		userId,
 	 	contact_name,
  		contact_type,
  		phone_number,
 		 birth_date
	);
    COMMIT;
        START TRANSACTION;
		INSERT INTO 
  		`reservations`
		(
  		`reservations`.`Id`,
  		`reservations`.`Details`,
  		`reservations`.`IsFavorite`,
  		`reservations`.`Ranking`,
  		`reservations`.`UserId`
		) 
		VALUE (
  		reservationId,
 	 	details,
  		isFavorite,
  		ranking,
 		 userId
	);
    COMMIT;
	End IF;
END$$

#
# Definition for the `get_all_reservations` procedure : 
#

DROP PROCEDURE IF EXISTS `get_all_reservations`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `get_all_reservations`(
        IN current_page INTEGER,
        IN total_records INTEGER,
        IN order_criteria INTEGER
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN

IF order_criteria =0
THEN
	SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
 	LIMIT total_records OFFSET current_page;
ELSEIF order_criteria=1
THEN
	SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  u.`BirthDate` ASC
 	LIMIT total_records OFFSET current_page;
ELSEIF order_criteria = 2
THEN
		SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  u.`BirthDate` DESC
 	LIMIT total_records OFFSET current_page;
ELSEIF order_criteria = 3
THEN
		SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  u.`ContactName` ASC
 	LIMIT total_records OFFSET current_page;
ELSEIF order_criteria = 4
THEN
		SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  u.`BirthDate` DESC
 	LIMIT total_records OFFSET current_page;
ELSEIF order_criteria=5
THEN 
		SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  r.`Ranking` ASC
 	LIMIT total_records OFFSET current_page;
ELSE
	SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
	r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
	FROM `users` u
	INNER JOIN `usertypes` d
	ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
    ORDER BY  r.`Ranking` DESC
 	LIMIT total_records OFFSET current_page;
END IF;
END$$

#
# Definition for the `get_all_reservations_by_user` procedure : 
#

DROP PROCEDURE IF EXISTS `get_all_reservations_by_user`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `get_all_reservations_by_user`(
        IN current_page INTEGER,
        IN total_records INTEGER,
        IN order_criteria INTEGER,
        IN user_id VARCHAR(36)
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN

SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`
FROM `users` u
INNER JOIN `usertypes` d
ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
WHERE u.`Id`=user_id
 LIMIT total_records OFFSET current_page;
 
END$$

#
# Definition for the `get_all_reservations_count` procedure : 
#

DROP PROCEDURE IF EXISTS `get_all_reservations_count`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `get_all_reservations_count`()
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN

SELECT COUNT(r.`Id`)AS counter
FROM  `reservations` r ;

END$$

#
# Definition for the `get_full_user_info` procedure : 
#

DROP PROCEDURE IF EXISTS `get_full_user_info`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `get_full_user_info`(
        IN contact_name VARCHAR(20),
        IN current_page INTEGER,
        IN total_records INTEGER
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN
SELECT u.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
r.`Description`,r.`Ranking`
FROM `users` u
INNER JOIN `usertypes` d
ON u.`ContactType` = d.`Id` INNER JOIN `reservations` r ON u.`Id`=r.`UserId`
WHERE u.`ContactName` = contact_name LIMIT total_records OFFSET current_page;
 
END$$

#
# Definition for the `get_reservation_by_id` procedure : 
#

DROP PROCEDURE IF EXISTS `get_reservation_by_id`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `get_reservation_by_id`(
        IN reservation_id VARCHAR(36)
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN

SELECT r.`Id`, u.`ContactName`, u.`BirthDate`,u.`PhoneNumber`, d.`Description`,
r.`Details`,r.`Ranking`,r.`IsFavorite`,r.`UserId`,d.`Id` as `user_typeId`
FROM `reservations` r 

INNER JOIN `users` u
ON r.`UserId`=u.`Id`
  INNER JOIN `usertypes` d ON  u.`ContactType` = d.`Id` 
  
  WHERE r.`Id` = reservation_id;  

END$$

#
# Definition for the `mark_as_favorite` procedure : 
#

DROP PROCEDURE IF EXISTS `mark_as_favorite`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `mark_as_favorite`(
        IN isFav BOOLEAN,
        iN reservationId VARCHAR(36)
    )
    NOT DETERMINISTIC
    MODIFIES SQL DATA
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN
START TRANSACTION;
 UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = isFav WHERE  `reservations`.`Id` = reservationId; 
 commit;
 Select * from `reservations`;

END$$

#
# Definition for the `user_exist` procedure : 
#

DROP PROCEDURE IF EXISTS `user_exist`$$

CREATE DEFINER = 'root'@'localhost' PROCEDURE `user_exist`(
        IN cad VARCHAR(20)
    )
    NOT DETERMINISTIC
    CONTAINS SQL
    SQL SECURITY DEFINER
    COMMENT ''
BEGIN
  Select (exists(select `Id` from `users` WHERE `users`.`ContactName` = cad)) AS user_exist;
END$$

DELIMITER ;

#
# Data for the `usertypes` table  (LIMIT 0,500)
#

INSERT INTO `usertypes` (`Id`, `Description`) VALUES 
  (1,'UserType 1'),
  (2,'UserType 2'),
  (3,'UserType 3');
COMMIT;



/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;