-- MySQL dump 10.13  Distrib 5.6.28, for Win64 (x86_64)
--
-- Host: localhost    Database: SCHOOL
-- ------------------------------------------------------
-- Server version	5.6.28-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `action`
--

DROP TABLE IF EXISTS `action`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `action` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '№ действия',
  `name` varchar(255) NOT NULL DEFAULT '' COMMENT 'Код действия для проверки',
  `info` text NOT NULL COMMENT 'Что включает в себя это действие',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '0-отключено, 1-работает',
  PRIMARY KEY (`id`),
  UNIQUE KEY `Индекс 2` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Список всех возможных действий в системе';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `action`
--

LOCK TABLES `action` WRITE;
/*!40000 ALTER TABLE `action` DISABLE KEYS */;
INSERT INTO `action` VALUES (1,'user_Update','-',1),(2,'role_Update','-',1),(3,'role_EditUser','-',1),(4,'role_EditAction','-',1),(5,'action_Update','Info',1);
/*!40000 ALTER TABLE `action` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `menu`
--

DROP TABLE IF EXISTS `menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '№ записи',
  `main_id` int(11) NOT NULL COMMENT 'Код блока меню',
  `menu` varchar(255) NOT NULL COMMENT 'Системное имя пункта',
  `href` varchar(255) NOT NULL COMMENT 'Ссылка пункта меню',
  `name` varchar(255) NOT NULL COMMENT 'Текст отображения',
  `info` varchar(255) DEFAULT NULL COMMENT 'Всплывающая подсказка',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '0-скрыто, 1-открыто',
  `nr` int(11) NOT NULL DEFAULT '0' COMMENT 'Порядок размещения пунктов',
  PRIMARY KEY (`id`),
  UNIQUE KEY `Индекс 2` (`menu`),
  KEY `FK_menu_menu_main` (`main_id`),
  CONSTRAINT `FK_menu_menu_main` FOREIGN KEY (`main_id`) REFERENCES `menu_main` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Пункты динамического меню';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu`
--

LOCK TABLES `menu` WRITE;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` VALUES (1,1,'home_signup','/Login/Signup','Регистрация','Регистрация в системе',1,5),(2,1,'home_login','/Login/Index','Вход','Авторизация',1,10),(3,1,'home_menus','/Cabinet/MenusList','Меню','Формирование меню',1,30);
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `menu_main`
--

DROP TABLE IF EXISTS `menu_main`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu_main` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '№ списка меню',
  `main` varchar(255) NOT NULL COMMENT 'Системный код списка',
  `name` varchar(255) DEFAULT NULL COMMENT 'Название списка',
  `info` text COMMENT 'Описание раздела',
  PRIMARY KEY (`id`),
  UNIQUE KEY `main` (`main`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Все списки меню';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu_main`
--

LOCK TABLES `menu_main` WRITE;
/*!40000 ALTER TABLE `menu_main` DISABLE KEYS */;
INSERT INTO `menu_main` VALUES (1,'HOME','Главное меню',NULL);
/*!40000 ALTER TABLE `menu_main` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '№ роли',
  `name` varchar(255) NOT NULL DEFAULT '' COMMENT 'Название роли',
  `info` text COMMENT 'Описание ролевой сути',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Список ролевых статусов проекта';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Administrator','-'),(2,'Support','-'),(3,'Teacher','-');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_action`
--

DROP TABLE IF EXISTS `role_action`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_action` (
  `role_id` int(11) NOT NULL COMMENT '№ роли',
  `action_id` int(11) NOT NULL COMMENT '№ действия',
  PRIMARY KEY (`role_id`,`action_id`),
  KEY `FK_role_action_action` (`action_id`),
  CONSTRAINT `FK_role_action_action` FOREIGN KEY (`action_id`) REFERENCES `action` (`id`),
  CONSTRAINT `FK_role_action_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Список доступных действий для каждой роли';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_action`
--

LOCK TABLES `role_action` WRITE;
/*!40000 ALTER TABLE `role_action` DISABLE KEYS */;
INSERT INTO `role_action` VALUES (1,2),(1,4);
/*!40000 ALTER TABLE `role_action` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_user`
--

DROP TABLE IF EXISTS `role_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_user` (
  `role_id` int(11) NOT NULL COMMENT '№ роли',
  `user_id` int(11) NOT NULL COMMENT '№ пользователя',
  PRIMARY KEY (`role_id`,`user_id`),
  KEY `FK_role_user_user` (`user_id`),
  CONSTRAINT `FK__role` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`),
  CONSTRAINT `FK_role_user_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Какой пользователь какой ролью обладает';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_user`
--

LOCK TABLES `role_user` WRITE;
/*!40000 ALTER TABLE `role_user` DISABLE KEYS */;
/*!40000 ALTER TABLE `role_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '№ пользователя',
  `name` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL COMMENT 'Электропочта для авторизации',
  `passw` varchar(255) DEFAULT NULL COMMENT 'Закодированный пароль',
  `status` int(11) DEFAULT '0' COMMENT '0-нет доступа, 1-есть',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Список всех пользователей системы';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Magic','fformula@gmail.com','*6BA30F8FDEEC669B962CDE659268DB536A9D2A2C',1),(2,'Valera','walera@yandex.ru','*292FDBC78273BECE9E50C2FC2CB1BEA494B0425C',1),(3,'Olia','hely@muza.org','*7820354FA39E9B967F91EA31D397DC1E788D4D43',1),(4,'Michael','misha@moskva.ru','*E75A663C6384E4833CBD81F37797B862017C2555',1),(5,'Abc111','aaa@aaa.aaa','*E75A663C6384E4833CBD81F37797B862017C2555',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_address`
--

DROP TABLE IF EXISTS `user_address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_address` (
  `user_id` int(11) NOT NULL COMMENT '№ пользователя',
  `country` varchar(255) DEFAULT NULL COMMENT 'Страна проживания',
  `zip` varchar(255) DEFAULT NULL COMMENT 'Почтовый индекс',
  `area` varchar(255) DEFAULT NULL COMMENT 'Область',
  `city` varchar(255) DEFAULT NULL COMMENT 'Город',
  `street` varchar(255) DEFAULT NULL COMMENT 'Улица, дом, квартира',
  `personal` varchar(255) DEFAULT NULL COMMENT 'Получатель письма',
  KEY `FK_user_address_user` (`user_id`),
  CONSTRAINT `FK_user_address_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Физический почтовый адрес';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_address`
--

LOCK TABLES `user_address` WRITE;
/*!40000 ALTER TABLE `user_address` DISABLE KEYS */;
INSERT INTO `user_address` VALUES (1,'Lithuania','3200','Area','Висагинас','Street','Personal'),(2,'333','333','444','444','555','555');
/*!40000 ALTER TABLE `user_address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_passw`
--

DROP TABLE IF EXISTS `user_passw`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_passw` (
  `user_id` int(11) NOT NULL COMMENT '№ пользователя',
  `passw` varchar(255) NOT NULL COMMENT 'Новый пароль (закодированный)',
  `code` varchar(255) NOT NULL COMMENT 'Код активации нового пароля',
  `request_date` datetime NOT NULL COMMENT 'Дата запроса, для удаления устаревших записей',
  KEY `FK_user_passw_user` (`user_id`),
  CONSTRAINT `FK_user_passw_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Таблица для запроса новых паролей на время их активации';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_passw`
--

LOCK TABLES `user_passw` WRITE;
/*!40000 ALTER TABLE `user_passw` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_passw` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-03-23 10:08:48
