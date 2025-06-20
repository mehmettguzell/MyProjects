# CREATE COMMANDS:

```sql
-- TYPE TABLE:
CREATE TABLE TYPE (
    TYPE_NO INT PRIMARY KEY,
    TYPE_NAME VARCHAR(100)
);
```
```sql
-- AUTHOR TABLE:
CREATE TABLE AUTHOR(
    AUTHOR_ID NCHAR(9) NOT NULL UNIQUE,
    AUTHOR_NAME VARCHAR(20) NOT NULL,
    AUTHOR_SURNAME VARCHAR(20)NOT NULL,
    AUTHOR_AGE SMALLINT	NOT NULL
    PRIMARY KEY(AUTHOR_ID)
    );
```
```sql
-- UNIVERSITY TABLE: 
CREATE TABLE UNIVERSITY(
    UNI_NO VARCHAR(5) NOT NULL UNIQUE,
    UNI_NAME VARCHAR(50)NOT NULL,
    PRIMARY KEY(UNI_NO)
    );
```
```sql
-- INSTITUTE TABLE:
CREATE TABLE INSTITUTE(
    T_INSTITUTE VARCHAR(4) NOT NULL PRIMARY KEY,
    T_INS_NAME VARCHAR(30)NOT NULL,
    UNI_NO varchar(4)NOT NULL,
    FOREIGN KEY (UNI_NO) REFERENCES UNIVERSITY(UNI_NO),
    );
```
```sql
-- LANGUAGE TABLE:
CREATE TABLE LANGUAGE(
    T_LANGUAGE_NO NCHAR(1) NOT NULL PRIMARY KEY,
    T_LANGUAGE VARCHAR(20) 
    ); 
```
```sql
-- CO_SUPERVISOR TABLE:
CREATE TABLE CO_SUPERVISOR(
    CS_ID NCHAR(5) NOT NULL PRIMARY KEY,
    CS_NAME VARCHAR(20) NOT NULL,
    )
```
```sql
-- THESIS TABLE:
CREATE TABLE THESIS(	
	THESIS_NUM varchar(7) NOT NULL PRIMARY KEY,
    T_TITLE VARCHAR(500)NOT NULL,
    T_ABSTRACT VARCHAR(5000)NOT NULL,
    D_DATE DATE	NOT NULL,
    T_YEAR NCHAR(4),
    TYPE_NO INT NOT NULL,
    T_PAGE SMALLINT NOT NULL,
    T_LANGUAGE_NO nchar(1)NOT NULL,
    UNI_NO varchar(5) NOT NULL,
    AUTHOR_ID nchar(9) NOT NULL,
    T_INSTITUTE varchar(4) NOT NULL,
    CS_ID nchar(5),
    FOREIGN KEY (TYPE_NO) REFERENCES TYPE(TYPE_NO),
    FOREIGN KEY (T_LANGUAGE_NO) REFERENCES LANGUAGE(T_LANGUAGE_NO),
    FOREIGN KEY (UNI_NO) REFERENCES UNIVERSITY(UNI_NO),
    FOREIGN KEY (AUTHOR_ID) REFERENCES AUTHOR(AUTHOR_ID),
    FOREIGN KEY (T_INSTITUTE) REFERENCES INSTITUTE(T_INSTITUTE),
    FOREIGN KEY (CS_ID) REFERENCES CO_SUPERVISOR(CS_ID)
);
```
```sql
-- KEYWORD TABLE:
CREATE TABLE KEYWORD(
    S_key varchar(20) NOT NULL PRIMARY KEY,
    THESIS_NUM varchar(7) NOT NULL,
	FOREIGN KEY (THESIS_NUM) REFERENCES THESIS(THESIS_NUM));
```
```sql
-- TOPIC_LIST TABLE:
CREATE TABLE TOPIC_LIST(
    TOPIC_ID NCHAR(10) NOT NULL PRIMARY KEY,
    TOPIC_ELEMENT VARCHAR(30),
)
```
```sql
-- LIST TABLE:
CREATE TABLE LIST (
    	THESIS_NUM varchar(7) NOT NULL,
    	TOPIC_ID varchar(10) NOT NULL,
    	FOREIGN KEY (TOPIC_ID) REFERENCES TOPIC_LIST(TOPIC_ID),
    	FOREIGN KEY (THESIS_NUM) REFERENCES THESIS(THESIS_NUM),
PRIMARY KEY (THESIS_NUM, TOPIC_ID)
);
```
```sql
-- SUPERVISOR TABLE:
CREATE TABLE SUPERVISOR(
    S_ID NCHAR(4) NOT NULL PRIMARY KEY,
    S_NAME VARCHAR(20) NOT NULL);
```
```sql
-- ADVISING TABLE:
CREATE TABLE ADVISING(
        S_ID nchar(4) NOT NULL UNIQUE,
        THESIS_NUM varchar(7) NOT NULL,

        PRIMARY KEY(S_ID, THESIS_NUM)
        FOREIGN KEY (S_ID) REFERENCES SUPERVISOR(S_ID),
        FOREIGN KEY (THESIS_NUM) REFERENCES THESIS(THESIS_NUM));
```
# INSERT COMMANDS:

```sql
-- TYPE TABLE:
INSERT INTO 
	TYPE (TYPE_NO, TYPE_NAME)
	VALUES 
		(1,'Master'),
		(2,'Doctorate'),
		(3,'Specialization_in_Medicine'),
(4,'Proficiency in Art');
```
```sql
-- AUTHOR TABLE:
INSERT INTO 
    AUTHOR (AUTHOR_ID, AUTHOR_NAME, AUTHOR_SURNAME, AUTHOR_AGE)
VALUES
    (210706012, 'JACK', 'JONES', 24),
    (210706015, 'UGUR', 'YESILYAPRAK', 23),
    (210706025, 'BERKAY', 'ARSLAN', 22),
    (210706030, 'MEHMET', 'GUZEL', 21),
    (220706035, 'NISA', 'SONMEZ', 22);
```
```sql
-- UNIVERSITY TABLE:
INSERT INTO 
    UNIVERSITY (UNI_NO, UNI_NAME)
VALUES
    ('BOUN', 'BOGAZICI'),
    ('DEN', 'DENEME'),
    ('MAU', 'MALTEPE'),
    ('MU', 'MARMARA'),
    ('OZU', 'OZYEGIN'),
    ('YEU', 'YEDITEPE'),
    ('YTU', 'YILDIZ_TEKNIK');
```
```sql
-- INSTITUTE TABLE:
INSERT INTO 
    INSTITUTE (T_INSTITUTE, T_INS_NAME, UNI_NO)
VALUES
    (1, 'SOCIAL SCIENCES', 'MAU'),
    (2, 'SCIENCE', 'OZU'),
    (3, 'AVIATION', 'BOUN'),
    (4, 'DISASTER MANAGEMENT', 'BOUN'),
    (5, 'INFORMATICS', 'MU'),
    (6, 'SPORTS MANAGEMENT', 'MU'),
    (7, 'HUMAN SCIENCES', 'YEU');
```
```sql
-- LANGUAGE TABLE:
INSERT INTO 
    LANGUAGE (T_LANGUAGE_NO, T_LANGUAGE)
VALUES
    (1, 'TURKISH'),
    (2, 'FRENCH'),
    (3, 'ENGLISH'),
    (4, 'SPANISH'),
    (5, 'GERMAN');
```
```sql
-- CO_SUPERVISOR TABLE:
INSERT INTO 
    CO_SUPERVISOR (CS_ID, CS_NAME)
VALUES
    ('CS101', 'ALI'),
    ('CS102', 'VELI'),
    ('CS103', 'FATMA'),
    ('CS104', 'CEMIL'),
    ('CS105', 'TURGUT');
```
```sql

-- THESIS TABLE:
1.
INSERT INTO THESIS
(THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE, CS_ID)
VALUES 
	('T1', 
	 'INVESTIGATION OF TERATOGENIC EFFECTS', 
	 'Bisphenol A (BPA), which has a wide use in the industry, is one of the most produced chemicals in the world. BPA takes part in the products such as food, personal care products, detergents, plastic bottles. In studies performed, BPA was identified in amniotic fluid, maternal and fetal plasma, placenta and mother\"s milk, fat tissue, semen, colostrum, and saliva. The aim of research is to determine the rate of cartilage/bone that belongs to forelimbs and hindlimbs of fetus whose mothers were exposed to BPA. In this study, 16 adult pregnant female Sprague Dawley rats were used. Rats were divided into 4 groups; the control group, low (0.5 mg/kg/day BPA), medium (5 mg/kg/day BPA), and high dose (50 mg/kg/day BPA) BPA groups. Indicated BPA doses were given between first and 20th day of gestation. The fetuses were removed out on the 20th day of pregnancy by cesarean. Skeletal system development of fetuses was examined with double and immunohistochemical staining (the tartrate resistant acid phosphatase (TRAP) and the alkaline phosphatase (AP) expressions) methods. The rate of bone-cartilage in forelimb (humerus, ulna, radius) and hindlimb bones (femur, tibia, fibula) were determined with ImageJ program and data was analyzed using SPSS statistical software. While the most ossification rates of humerus, radius, and ulna were detected as 41.05%, 39.25%, 37.26% in the control group, the least ossification rates were detected as 31.80%, 30.24%, 35.03% in the high dose BPA group respectively. The differences among the groups that related to the ossification areas and the lengths of humerus, radius, ulna were determined statistically significant (p<0.001). While the most ossification rates of femur, tibia, and fibula were detected as 23.04%, 30.73%, 32.78% in the control group, the least ossification rates were detected as 17.75%, 20.90%, 24.32% in the high dose BPA group respectively. The differences among the control and experimental groups that related to TRAP and AP expressions of femur were determined statistically significant by using immunohistochemical staining (p=0.000). According to these data, exposure to BPA during pregnancy reduces the ossification in the skeletal system and affects bone growth negatively. The decrease in the ossification rate of examined bones was in a dose-dependent manner."', 
	 '2024-07-01', 
	 2024, 
	 'Master', 
	 250, 
	 1, 
	 'MAU', 
	 210706012, 
	 1, 
	 'CS101');




2. 
INSERT INTO THESIS
	(THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE, CS_ID)
	VALUES 
		('T2', 
		 'ANALYSIS OF FACTORS AFFECTING TEAM PERFORMANCE IN BASKETBALL', 
		 'Efficiency is an important factor in the sporting production process of a sports team. This means that a team having highly skilled and qualified players, excellent facilities and similar entrances does not mean that the team will achieve the highest performance. It is therefore important to use inputs efficiently for a sport team. In this study, the efficiency of basketball teams as a measure of sportive performance and performance was investigated. To study sporting production performance and efficiency in terms of basketball teams, American National Basketball Association League 2015-2016, short name WNBA, was chosen as an example. A two-stage emprical strategy has been adopted to achieve the objectives we have identified. In the first stage, the production of WNBA teams and the inputs that determine this production were analyzed with the help of three different production functions created by us. In the second stage, the inputs obtained in the first stage were analyzed with the help of the stochastic frontier analysis for the same teams. True random effect model is used in the efficiency calculations. As a result of the analyzes made, the average efficiency of WNBA teams were calculated to be close to 1. In the light of these findings, WNBA teams achieved a high level of performance in using the inputs they possessed efficiently.', 
		 '2023-03-10', 
		 2024, 
		 'Doctorate', 
		 350, 
		 2, 
		 'MU', 
		 210706030, 
		 6, 
		 'CS102');

3. 
 INSERT INTO THESIS
	(THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE, CS_ID)
	VALUES 
		('T3', 
		 'THE EFFECT OF CHILD NEGLIGENCE AND ABUSE TRAINING GIVEN TO HEALTH VOCATIONAL HIGH SCHOOL TEACHERS ON THEIR KNOWLEDGE AND AWARENESS', 
		 'This research which is descriptive and segmental is carried on medical health high school teachers’ to evaluate their knowledge and awareness after being informed on the child abuse and negligence. The survey carried on 215 teachers who work at government school and private school of Kayseri Public Education Directorate between September 2016 and April 2017. The required Approval were received from Erciyes University Medical Faculty Clinic Researches ethical commission and Kayseri Public Education Directorate. Teachers who accepted to join the survey were educated on child abuse and negligence. Data’s were obtained by way of survey sheets which is prepared by the researcher and the scale that diagnose the symptoms and risk of the child abuse and negligence. Data’s were evaluated by computer and the averages are showed by way at standard drift and percentage. Ki-square and the difference between two averages importance test and variance analyses were used in the statistical analyses. At the end p<0.05 was accepted meaningful for statistical angle. %63.7 of the teachers joined to the survey were female; %80.5 are married. The average age of the teachers’ were 40.5 ± 9.9 years. %87.4 percent of the teachers’ found the education that given by the researcher enough. It was seen that the teachers faced most with the physical abuse (%55.4). It was seen that on condition that teachers were exposed to one of those abuses, they will declare (%98.1). And the first place would be directory of the schools. As a result, there aren’t seen any awareness affects of the age and average working year like specifications of the teachers. The total point that female teachers took from the survey was more than the male ones and the difference between them was found meaningful by the statistics (p<0.05). It was found that the highest points in the survey was taken by the health teachers.', 
		 '2025-04-25', 
		 2024, 
		 'Specialization in Medicine', 
		 450, 
		 2, 
		 'OZU', 
		 220706035, 
		 2, 
		 NULL);

4. 

INSERT INTO THESIS 
	(THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE, CS_ID)
	VALUES 
		('T4', 
		 'INVESTIGATION OF DIGITAL GAME ADDICTION, SOCIAL ANXIETY AND ALEXITMIA LEVELS IN ADOLESCENTS', 
		 'Nowadays, due to rapid technological advances, children and adolescents prefer digital games played with technological tools. Digital games which played uncontrolled can create addiction in adolescents and can lead to the development of social anxiety by affecting communication skills negatively. This may result in the development of alexithymic personality traits, which are described as "not finding promises in emotions" in adolescents. This study was planned as descriptive to examine the levels of adolescents digital game addiction, social anxiety, and alexithymia. The study was conducted with 626 adolescents aged between 12 and 15 years who wanted to participate and whose parents approved. Ethical Committee, institutional permission, and consent from both adolescents and their parents were obtained for the research. Data were collected through “Introductory Features Form”, “Digital Game Addiction Scale (DGAS)”, “Social Anxiety Scale For Adolescents (SAS-A)”, and “Alexithymia Scale for Children (ASC)”. Descriptive statistics, independent t-test, Mann-Whitney U, ANOVA, Kruskal-Wallis tests, and Spearman correlation analysis were used for data evaluation. Mean age of participants was 13.20 ± 0.79 years and 62.6% were male. The mean score of DGAS was higher in male adolescents, those using the internet for over three hours a day, and those playing strategy games. SAS-A scores were higher in adolescents aged 12-13 and those with low socioeconomic status. Adolescents without smartphones or internet access on their phones, those playing simulation games, and those with health problems also scored higher on SAS-A. Conversely, SAS-A scores were lower in adolescents whose mothers had a high school education or fathers had education levels above university. ASC scores were higher in adolescents whose mothers worked as laborers, had low socioeconomic status, good academic performance, and health problems. Additionally, a positive correlation was observed between DGAS, SAS-A, and ASC scores. Based on these findings, addressing issues like digital game addiction, social anxiety, and alexithymia in adolescents and implementing appropriate interventions is recommended."', 
		 '2026-05-25', 
		 2024, 
		 'Master', 
		 280, 
		 2, 
		 'YEU', 
		 210706025, 
		 7, 
		 'CS103');




5.
INSERT INTO THESIS 
    (THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE, CS_ID)
VALUES 
    ('T5', 
     'DEVELOPMENT OF WEB-BASED BIOINFORMATICS TOOLS FOR ANALYSIS OF NEXT-GENERATION SEQUENCE DATA OF THE HUMAN GENOME', 
     'Developments in DNA sequencing technologies, have accelerated the life sciences research and have made considerable contributions to the clinical / industrial field routine. With the reduction in the cost of sequencing per base pairs, and the sequencing democratization, the next generation sequencing technology has been widely used in small and medium-sized laboratories. However, the fact that the data obtained after sequencing is too large to be processed manually and the large labor-intensive study burden reveals the need for relevant bioinformatics software and automated analysis pipelines. In this thesis study, which was realized in the direction of need, a web-based bioinformatic tool-pipeline was developed for the analysis of new generation sequencing data of the human genome. In the study, a variant calling analysis pipeline was created to analyze the data obtained from the new generation sequencing and then this analysis pipeline was made web-based. The software has a simple interface that is easy to use for the end user. In the annotation of the variant information obtained from the analysis pipeline as HGVCT, using a data pool compiled from different databases, so that the information obtained is provided to a wider range of different needs. For the users who want to perform processes other than the analysis presented, all the files obtained are presented to the users.', 
     '2027-09-12', 
     2025, 
     'DOCTORA', 
     1050, 
     2, 
     'OZU', 
     210706025, 
     2, 
     NULL);
```
```sql
-- KEYWORD TABLE:
INSERT INTO KEYWORD (S_key, THESIS_NUM)
VALUES
    ('KEY1', 'T1'),
    ('KEY2', 'T2'),
    ('KEY3', 'T3'),
    ('KEY4', 'T4'),
    ('KEY5', 'T5');
```
```sql
-- TOPIC_LIST TABLE:
INSERT INTO TOPIC_LIST (TOPIC_ID, TOPIC_ELEMENT)
VALUES
    ('TOPIC1', 'TERATOGENIC'),
    ('TOPIC2', 'BASKETBALL'),
    ('TOPIC3', 'CHILD NEGLIGENCE AWARENESS'),
    ('TOPIC4', 'ADOLESCENCE AND ADDICTION'),
    ('TOPIC5', 'BIOTECHNOLOGY');
```
```sql
-- LIST TABLE:
INSERT INTO LIST (THESIS_NUM, TOPIC_ID)
VALUES
    ('T1', 'TOPIC1'),
    ('T2', 'TOPIC2'),
    ('T3', 'TOPIC3'),
    ('T4', 'TOPIC4'),
    ('T5', 'TOPIC5');
```
```sql
-- SUPERVISOR TABLE:
INSERT INTO SUPERVISOR (S_ID, S_NAME)
VALUES
    ('S101', 'VOLKAN'),
    ('S102', 'RAIF'),
    ('S103', 'BERK'),
    ('S104', 'ENSAR'),
    ('S105', 'EMRE');
```
```sql
-- ADVISING TABLE:
INSERT INTO ADVISING (S_ID, THESIS_NUM)
VALUES
    ('S101', 'T1'),
    ('S101', 'T3'),
    ('S102', 'T2'),
    ('S103', 'T4'),
    ('S105', 'T5');
```