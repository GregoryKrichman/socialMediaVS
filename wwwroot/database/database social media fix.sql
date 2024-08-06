-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: social
-- ------------------------------------------------------
-- Server version	8.0.37

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240621214341_InitialCreate','7.0.20'),('20240621231726_UpdatedInitialCreate','7.0.20'),('20240708172312_InitialCreate1','7.0.20'),('20240708181029_UpdateJsonHandling','7.0.20'),('20240708190601_UpdateModels','7.0.20');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comments` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Desc` varchar(200) DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UserId` int NOT NULL,
  `PostId` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `postId_idx` (`PostId`),
  KEY `commentUserId_idx` (`UserId`),
  CONSTRAINT `commentUserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `postId` FOREIGN KEY (`PostId`) REFERENCES `posts` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
INSERT INTO `comments` VALUES (3,'hi','2024-06-14 19:40:58.000000',1,14),(4,'Sample Comment11','2024-07-08 19:40:12.097626',78,48),(5,'Sample Comment11','2024-07-09 16:38:19.675074',78,46),(10,'test123','2024-07-09 18:27:49.328029',83,2),(11,'rr','2024-07-09 19:10:48.263211',84,15),(12,'test1234','2024-07-10 14:59:39.532807',85,2),(13,'1','2024-07-11 20:39:43.954828',90,68),(14,'1','2024-07-11 20:50:10.628776',90,70),(15,'11','2024-07-11 21:21:24.977600',90,70),(16,'hi2','2024-07-12 21:01:02.067304',90,72),(17,'123','2024-07-12 21:06:16.512579',90,72),(18,'2222222223','2024-07-12 21:06:27.829115',90,72),(19,'2','2024-07-12 21:07:52.554587',90,72),(20,'2','2024-07-12 21:08:25.908018',90,73),(21,'1','2024-07-12 21:08:27.478218',90,73),(22,'11','2024-07-13 16:43:13.298048',90,73),(23,'tt','2024-07-13 17:12:31.941609',90,75),(24,'hi','2024-07-14 11:22:18.608678',90,73),(25,'tt','2024-07-14 11:49:49.985785',90,72),(26,'test','2024-07-14 16:55:06.212820',90,82);
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `likes`
--

DROP TABLE IF EXISTS `likes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `likes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `PostId` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `likeUserId_idx` (`UserId`),
  KEY `likePostId_idx` (`PostId`),
  CONSTRAINT `likePostId` FOREIGN KEY (`PostId`) REFERENCES `posts` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `likeUserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `likes`
--

LOCK TABLES `likes` WRITE;
/*!40000 ALTER TABLE `likes` DISABLE KEYS */;
INSERT INTO `likes` VALUES (7,77,48),(8,78,48),(9,78,46),(10,78,47),(11,78,47),(17,89,3),(21,89,54),(27,89,16),(28,89,15),(29,89,14),(31,89,62),(52,90,75);
/*!40000 ALTER TABLE `likes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Desc` varchar(200) DEFAULT NULL,
  `Img` varchar(200) DEFAULT NULL,
  `UserId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `Content` longtext,
  PRIMARY KEY (`id`),
  KEY `userId_idx` (`UserId`),
  CONSTRAINT `userId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=83 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts`
--

LOCK TABLES `posts` WRITE;
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
INSERT INTO `posts` VALUES (2,'test1','default.jpg',2,'2024-06-28 20:42:58.000000',''),(3,'test2','default.jpg',3,'2024-06-28 20:42:58.000000',''),(14,'from insomnia','default.jpg',1,'2024-06-11 23:07:04.000000',''),(15,'from insomnia gyt','default.jpg',13,'2024-06-12 18:11:38.000000',''),(16,'from insomnia good job','default.jpg',13,'2024-06-12 18:11:58.000000',''),(41,'hi greg','default.jpg',13,'2024-06-14 20:20:15.000000',''),(42,'hi greg','default.jpg',15,'2024-06-14 20:25:06.000000',''),(43,'This is a test post','path/to/image.jpg',1,'2024-07-08 17:48:29.345493','Post content here'),(44,'This is a test post','path/to/image.jpg',1,'2024-07-08 17:52:45.242328','Post content here'),(45,'This is a test post','path/to/image.jpg',75,'2024-07-08 17:58:46.185014','Post content here'),(46,'This is a test post','',75,'2024-07-08 17:58:58.991723','Post content here'),(47,'Sample Post','image_url',77,'2024-07-08 18:19:52.514957','This is a sample post'),(48,'Sample Post2','image_url',77,'2024-07-08 18:35:09.158772','This is a sample post'),(49,'Sample Post','image_url',78,'2024-07-08 19:11:47.424485','This is a sample post'),(50,'Sample Post4','image_url',78,'2024-07-08 19:35:55.973859','This is a sample post4'),(53,'Sample Post5','image_url',82,'2024-07-09 16:39:24.623774','This is a sample post5'),(54,'test1234t','',84,'2024-07-09 18:34:15.988096','test1234t'),(55,'gtest2','',84,'2024-07-09 18:34:43.989977','gtest2'),(56,'test12r','',84,'2024-07-09 19:10:35.816155','test12r'),(57,'hi','',85,'2024-07-10 15:01:12.637263','hi'),(58,'hi','',86,'2024-07-10 17:02:10.342901','hi'),(59,'wer','',87,'2024-07-10 19:25:15.294143','wer'),(60,'hi','',88,'2024-07-10 21:22:14.102973','hi'),(61,'tesr','',89,'2024-07-11 15:45:27.201357','tesr'),(62,'hi','',89,'2024-07-11 17:09:24.489978','hi'),(63,'hit','',89,'2024-07-11 17:27:35.872357','hit'),(64,'good job','',89,'2024-07-11 18:03:24.035651','good job'),(65,'eeeee','',89,'2024-07-11 19:39:17.325057','eeeee'),(66,'test pic ','70eb26dd-4e48-4924-8761-6afe81bedc03.jpeg',89,'2024-07-11 19:39:34.156509','test pic '),(67,'erer','',89,'2024-07-11 19:44:33.546286','erer'),(68,'123','',89,'2024-07-11 19:53:53.527563','123'),(69,'1','',90,'2024-07-11 20:25:07.773228','1'),(70,'se','e2b4f3d0-91c3-4ce9-a5de-6e8caf8cd2cf.jpeg',90,'2024-07-11 20:25:21.304645','se'),(71,'test pic1','651a6ef3-5e6d-4bf7-837e-0e5fc636eb60.jpeg',90,'2024-07-12 19:11:24.531750','test pic1'),(72,'test pic 2','4f13eb3d-aeaa-48c7-9439-035fca56961b.jpg',90,'2024-07-12 20:51:27.786627','test pic 2'),(73,'2test pic','3a83421d-a92c-4a4b-8460-2b3b264f1cdc.jpg',90,'2024-07-12 21:08:12.419376','2test pic'),(74,'hi','',90,'2024-07-13 17:12:13.283101','hi'),(75,'test pic 3','b230d3b1-4089-4704-90a7-ff59ccbbb365.jpg',90,'2024-07-13 17:12:25.070489','test pic 3'),(76,'hi','',90,'2024-07-14 11:50:12.402601','hi'),(77,'pic test','33f413f5-0ac8-45c0-a739-c58e49cd37ea.jpg',90,'2024-07-14 11:50:27.838293','pic test'),(78,'hi','',90,'2024-07-14 13:39:35.591457','hi'),(79,'test','',91,'2024-07-14 14:32:54.484703','test'),(80,'pic test','bfbf755c-4fff-46f7-af80-5f0283781ba7.jpg',91,'2024-07-14 14:33:55.681115','pic test'),(81,'hi','',90,'2024-07-14 16:54:49.104049','hi'),(82,'test pic','d66d8569-ddce-47c9-a535-8ebfe2ee2a02.jpg',90,'2024-07-14 16:54:59.951528','test pic');
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `relationships`
--

DROP TABLE IF EXISTS `relationships`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `relationships` (
  `id` int NOT NULL AUTO_INCREMENT,
  `FollowerUserId` int NOT NULL,
  `FollowedUserId` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `followerUserId_idx` (`FollowerUserId`),
  KEY `follwedUserId_idx` (`FollowedUserId`),
  CONSTRAINT `followerUserId` FOREIGN KEY (`FollowerUserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `follwedUserId` FOREIGN KEY (`FollowedUserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `relationships`
--

LOCK TABLES `relationships` WRITE;
/*!40000 ALTER TABLE `relationships` DISABLE KEYS */;
INSERT INTO `relationships` VALUES (5,1,3),(7,1,2),(8,1,13),(9,1,15),(10,1,14),(11,1,1),(12,78,76),(26,88,1),(27,88,1),(28,88,1),(29,88,1),(30,88,1),(31,88,1),(32,88,1),(33,88,1),(34,88,1),(35,88,1),(36,88,1),(37,88,1),(40,89,1),(41,89,1),(42,89,87),(43,89,87),(44,89,85),(45,89,85),(46,89,85),(47,89,85),(49,89,15),(51,89,13),(52,89,13),(53,89,13),(54,89,13),(55,89,13),(56,89,77),(57,89,77),(58,89,1),(59,89,1),(60,89,1),(61,89,1),(62,89,1),(63,89,1),(64,89,1),(72,89,2),(73,89,1),(74,89,15),(75,89,13),(77,89,2),(85,90,1),(86,90,3),(88,90,16),(89,90,15),(90,90,3),(91,90,1),(95,90,1),(97,90,13),(98,90,1),(99,90,13);
/*!40000 ALTER TABLE `relationships` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stories`
--

DROP TABLE IF EXISTS `stories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stories` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Img` varchar(300) DEFAULT NULL,
  `UserId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `storyUserId_idx` (`UserId`),
  CONSTRAINT `storyUserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stories`
--

LOCK TABLES `stories` WRITE;
/*!40000 ALTER TABLE `stories` DISABLE KEYS */;
INSERT INTO `stories` VALUES (1,'1718382283572_random photo.jpeg',1,'2024-06-14 19:24:43.000000'),(2,'1718382302988_random photo.jpeg',1,'2024-06-14 19:25:02.000000'),(3,'1718382313413_random photo.jpeg',1,'2024-06-14 19:25:13.000000'),(4,'story_image_url',78,'2024-07-08 19:29:22.120320'),(5,'23a2ce28-44a2-4b85-8e0a-f73aa84c8ea9.jpg',90,'2024-07-12 22:58:34.722840'),(6,'aea06193-cdfd-4804-9917-3a412f90cc0b.jpg',90,'2024-07-12 22:58:39.874719'),(7,'edf2247f-da20-4f5c-8d38-dd76261c19ea.jpg',90,'2024-07-12 23:24:59.091140'),(8,'976829d6-7623-41c1-8537-63d552a4bed1.jpg',90,'2024-07-12 23:29:25.366200'),(9,'fda2f048-813d-4271-9bb2-4e2558996b84.jpg',90,'2024-07-12 23:32:42.857728'),(10,'0515dce6-1bf1-489c-9987-c7f5a4033925.jpg',90,'2024-07-12 23:34:18.708101'),(11,'e4fdc838-bc36-4864-a575-6e6d4094b3d4.jpg',90,'2024-07-12 23:34:35.592813'),(12,'81a06d52-8073-43e9-9da9-54eb4764f822.jpg',90,'2024-07-13 16:56:14.907951'),(13,'5719080b-e91c-4dc6-a6d2-761595a3c6a9.jpg',91,'2024-07-14 14:34:01.369152'),(14,'f6890dd4-822a-4fbc-b54e-3f2a93f89431.jpg',90,'2024-07-14 16:54:44.436614');
/*!40000 ALTER TABLE `stories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(200) NOT NULL,
  `name` varchar(45) NOT NULL,
  `CoverPic` varchar(500) DEFAULT NULL,
  `ProfilePic` varchar(500) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `Website` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'gregtest3','gregtest3@gmail.com','$2a$10$BIN3iV62VDfaeV.z9vrFq.NOlhsx/b.UN5K/pU6I1fMPVXE0zzuyW','greg','1718374429913_random photo.jpeg','1718374429905_random photo.jpeg','israel','google.com'),(2,'greg','test1@gmail.com','$2a$10$aCP84St7v58h/rexKB1LJuDWrgaDZ1552hVvuU2HskJ/PUGDl5YOi','test1','https://images.pexels.com/photos/13916254/pexels-photo-13916254.jpeg?auto=compress&cs=tinysrgb&w=1600&lazy=load','https://images.pexels.com/photos/4881619/pexels-photo-4881619.jpeg?auto=compress&cs=tinysrgb&w=1600',NULL,NULL),(3,'jon','gtest4@gmail.com','$2a$10$1bMzue0otf8vq.fZe2Ipi.zBPZnIG1J9thCNZRZCCgEfN8IDzcceW','test4','auto=compress&cs=tinysrgb&w=1600&lazy=load','https://images.pexels.com/photos/4881619/pexels-photo-4881619.jpeg?auto=compress&cs=tinysrgb&w=1600',NULL,NULL),(13,'test4','gtest4@gmail.com','$2a$10$EtFvoyzSWRzjoYSaSvoaKeoQUI5p0UcZ4juK7cN3P4X1t4Ow8qfnS','test4',NULL,NULL,NULL,NULL),(14,'jon hencock','gtest4@gmail.com','$2a$10$KBcQePcOD7uH74Bato6qAubBksETPPqsK1x80X3v8RvkYaMICvvva','on hencock',NULL,NULL,NULL,NULL),(15,'roni guter','gtest4@gmail.com','$2a$10$KZsNwd76Q5EgvuEIq964PeqDzuNGBCVrhcJuwKCUYtdZQQqx0nOQm','roni',NULL,NULL,NULL,NULL),(16,'testuser2','testuser2@example.com','password123','TestUser2','','','',''),(17,'testuser22','testuser22@example.com','password123','TestUser22','','','',''),(18,'testuser212','testuser212@example.com','password123','TestUser212','','','',''),(19,'testuser1234','testuser1234@gmail.com','123456','testuser1234','','','',''),(20,'testuser1234','testuser1234@gmail.com','123456','testuser1234','','','',''),(21,'testuser2122','testuser2122@example.com','password123','TestUser2122','','','',''),(22,'usertr','usertr@gmail.com','123456','usertr','','','',''),(23,'user123t','user123t','123456','usert123t','','','',''),(24,'user123t','user123t','123456','usert123t','','','',''),(25,'user123t','user123t','123456','usert123t','','','',''),(26,'user123t','user123t','123456','usert123t','','','',''),(27,'user123t','user123t','123456','usert123t','','','',''),(28,'user123t','user123t','123456','usert123t','','','',''),(29,'user123t','user123t','123456','usert123t','','','',''),(30,'user123t','user123t','123456','usert123t','','','',''),(31,'user123t','user123t','123456','usert123t','','','',''),(32,'user123t','user123t','123456','usert123t','','','',''),(33,'string','string','string','string','','','',''),(34,'testuser21222','testuser21222@example.com','password123','TestUser21222','','','',''),(35,'testuser212222','testuser212222@example.com','password123','TestUser212222','','','',''),(36,'testuser2122222','testuser2122222@example.com','password123','TestUser2122222','','','',''),(37,'testy','testy@gmail.com','123456','testy','','','',''),(38,'test99','test99@gmail.com','123456','test99','','','',''),(39,'gregtestg','gregtestg@gmail.com','123456','gregtestg','','','',''),(40,'testgk1','testgk1@gmail.com','123456','testgk1',NULL,NULL,NULL,NULL),(41,'gktest','gktest','123456','gktest',NULL,NULL,NULL,NULL),(42,'ftest1','ftest1@gmail.com','123456','ftest1',NULL,NULL,NULL,NULL),(43,'ftest2','ftest2@gmail.com','123456','ftest2',NULL,NULL,NULL,NULL),(44,'ftest3','ftest3@gmail.com','123456','ftest3',NULL,NULL,NULL,NULL),(45,'ftest4','ftest4','123456','ftest4',NULL,NULL,NULL,NULL),(46,'ftest5','ftest5','123456','ftest5',NULL,NULL,NULL,NULL),(47,'ftest6','ftest6@gmail.com','123456','ftest6',NULL,NULL,NULL,NULL),(48,'ftest7','ftest7@gmail.com','123456','ftest7',NULL,NULL,NULL,NULL),(49,'ftest8','ftest8@gmail.com','123456','ftest8',NULL,NULL,NULL,NULL),(50,'ftest10','ftest10@gmail.com','123456','ftest10',NULL,NULL,NULL,NULL),(51,'ftest11','ftest11@gmail.com','123456','ftest11',NULL,NULL,NULL,NULL),(52,'ftest12','ftest12@gmail.com','123456','ftest12',NULL,NULL,NULL,NULL),(53,'ftest13','ftest13@gmail.com','123456','ftest13',NULL,NULL,NULL,NULL),(54,'ftets15','ftets15@gmail.com','123456','ftets15',NULL,NULL,NULL,NULL),(55,'ftest16','ftest16@gmail.com','123456','ftest16',NULL,NULL,NULL,NULL),(56,'ftest17','ftest17@gmail.com','123456','ftest17',NULL,NULL,NULL,NULL),(57,'ftest18','ftest18@gmail.com','123456','ftest18',NULL,NULL,NULL,NULL),(58,'ftest19','ftest19@gmail.com','123456','ftest19',NULL,NULL,NULL,NULL),(59,'ftest20','ftest20@gmail.com','123456','ftest20',NULL,NULL,NULL,NULL),(60,'ftest21','ftest21@gmail.com','123456','ftest21',NULL,NULL,NULL,NULL),(61,'ftest22','ftest22@gmail.com','123456','ftest22',NULL,NULL,NULL,NULL),(62,'ttest','ttest@gmail.com','123456','ttest',NULL,NULL,NULL,NULL),(63,'ttest1','ttest1@gmail.com','123456','ttest1',NULL,NULL,NULL,NULL),(64,'ttest3','ttest3@gmail.com','123456','ttest3',NULL,NULL,NULL,NULL),(65,'ttest4','ttest4@gmail.com','12345','ttest4',NULL,NULL,NULL,NULL),(66,'ttest5','ttest5@gmail.com','123456','ttest5',NULL,NULL,NULL,NULL),(67,'ttest6','ttest6@gmail.com','123456','ttest6',NULL,NULL,NULL,NULL),(68,'gttest','gttest@gmail.com','123456','gttest',NULL,NULL,NULL,NULL),(69,'ttttest','ttttest@gmail.com','123456','ttttest',NULL,NULL,NULL,NULL),(70,'testuser7','testuser7@gmail.com','123456','testuser7',NULL,NULL,NULL,NULL),(71,'testuser21222222','testuser21222222@example.com','password123','TestUser21222222',NULL,NULL,NULL,NULL),(72,'testuser212222222','testuser212222222@example.com','password123','TestUser212222222',NULL,NULL,NULL,NULL),(73,'usertest1','usertest1@gmail.com','123456','usertest1',NULL,NULL,NULL,NULL),(74,'testuser6','test@example.com','password123','Test User',NULL,NULL,NULL,NULL),(75,'testuser6','test@example.com','password123','Test User',NULL,NULL,NULL,NULL),(76,'testuser','test@example.com','Password123!','Test User',NULL,NULL,NULL,NULL),(77,'testuser8','test@example.com','Password123!','Test User',NULL,NULL,NULL,NULL),(78,'testuser9','test@example.com','password123','Test User',NULL,NULL,NULL,NULL),(79,'usertest10','usertest10@gmail.com','123456','usertest10',NULL,NULL,NULL,NULL),(80,'usertes11','usertes11@gmail.com','123456','usertes11',NULL,NULL,NULL,NULL),(81,'usertest12','usertest12@gmail.com','123456','usertest12',NULL,NULL,NULL,NULL),(82,'usertest13','usertest13@gmail.con','123456','usertest13',NULL,NULL,NULL,NULL),(83,'usertest14','usertest14@gmail.com','123456','usertest14',NULL,NULL,NULL,NULL),(84,'usertest15','usertest15@gmail.com','123456','usertest15',NULL,NULL,NULL,NULL),(85,'usertest16','usertest16@gmail.com','123456','usertest16',NULL,NULL,NULL,NULL),(86,'usertest17','usertest17@gmail.com','123456','usertest17',NULL,NULL,NULL,NULL),(87,'usertest18','usertest18@gmail.com','123456','usertest17',NULL,NULL,NULL,NULL),(88,'usertest19','usertest19@gmail.com','123456','usertest19',NULL,NULL,NULL,NULL),(89,'usertest20','usertest20@gmail.com','123456','usertest20',NULL,NULL,NULL,NULL),(90,'usertest21','usertest21@gmail.com','123456','usertest232','f78cef7f-4772-4f19-be35-5bc9561958a2.jpg','ef32b315-f7e2-482c-b7f5-efab00e2130a.jpg','ramatgan1','www.google1.com'),(91,'admin','admin@example.com','AdminPassword123!','Admin User2','b975a771-fa23-40a3-83fc-21470dbf0f65.jpg','ac820f03-c617-4b5d-8caf-061c2efcb926.jpg','tel-aviv1','www.google1.com'),(92,'guest1','guest1@example.com','GuestPassword123!','Guest User 1',NULL,NULL,NULL,NULL),(93,'guest2','guest2@example.com','GuestPassword123!','Guest User 2',NULL,NULL,NULL,NULL),(94,'usertest22','usertest22@gmail.com','123456','usertest22',NULL,NULL,NULL,NULL),(95,'usertest24','usertest24@gmail.com','123456','usertest24',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-06 20:46:23
