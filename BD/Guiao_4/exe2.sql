-- Airport

CREATE TABLE AIRPORT (
    Airport_code    CHAR(3)         NOT NULL,
    City            VARCHAR(255)    NOT NULL,
    State           VARCHAR(255)    NOT NULL,
    Name            VARCHAR(255)    NOT NULL,
    PRIMARY KEY (Airport_code)
);

-- Airplane_type

CREATE TABLE AIRPLANE_TYPE (
    Type_name       VARCHAR(255),
    Max_seats       INT         ,
    Company         VARCHAR(255),
    Name            VARCHAR(255)    NOT NULL,
    PRIMARY KEY (Type_name)
);

-- Can_land

CREATE TABLE CAN_LAND (
    Airport_code    CHAR(3),
    Type_name       VARCHAR(255),
    PRIMARY KEY (Airport_code,Type_name),
    FOREIGN KEY (Airport_code) REFERENCES AIRPORT(Airport_code),
    FOREIGN KEY (Type_name) REFERENCES AIRPLANE_TYPE(Type_name)
);

-- Airplane

CREATE TABLE AIRPLANE (
    Airplane_id     INT             NOT NULL,
    Type_name       VARCHAR(255)    NOT NULL,
    Total_seats     INT,
    PRIMARY KEY (Airplane_id),
    FOREIGN KEY (Type_name) REFERENCES AIRPLANE_TYPE(Type_name)
);

-- Flight

CREATE TABLE FLIGHT (
    F_Number        INT             NOT NULL,
    Airline         VARCHAR(255)    NOT NULL,
    Weekdays        VARCHAR(255)    NOT NULL,
    PRIMARY KEY (F_Number)
);

-- Flight_leg

CREATE TABLE FLIGHT_LEG (
    Leg_no              INT         NOT NULL,
    Airport_code_dep    CHAR(3)     NOT NULL,
    Airport_code_arr    CHAR(3)     NOT NULL,
    Scheduled_dep_time  CHAR(5),
    Scheduled_arr_time  CHAR(5),
    Flight_Number       INT         NOT NULL,
    PRIMARY KEY (Leg_no,Flight_Number),
    FOREIGN KEY (Airport_code_dep) REFERENCES AIRPORT(Airport_code),
    FOREIGN KEY (Airport_code_arr) REFERENCES AIRPORT(Airport_code),
    FOREIGN KEY (Flight_Number) REFERENCES AIRPORT(F_Number)
);

-- Fare

CREATE TABLE FARE (
    Code            VARCHAR(60)    NOT NULL,
    Amount          INT            NOT NULL,
    Flight_Number   INT            NOT NULL,
    Restrictions    VARCHAR(100),
    PRIMARY KEY (Flight_Number,Code),
    FOREIGN KEY (Flight_Number) REFERENCES FLIGHT(F_Number)
);

-- Leg_instance

CREATE TABLE LEG_INSTANCE (
    Date                VARCHAR(25)     NOT NULL,
    Leg_no              INT             NOT NULL,
    Flight_Number       INT             NOT NULL,    
    No_of_avail_seats   INT,        
    Arr_time            CHAR(5),    
    Dep_time            CHAR(5),
    Airport_code_dep    CHAR(3)         NOT NULL,
    Airport_code_arr    CHAR(3)         NOT NULL,
    Airplane_id         INT             NOT NULL,
    PRIMARY KEY (Date,Leg_no,Flight_Number),
    --FOREIGN KEY (Airport_code_dep) REFERENCES AIRPORT(Airport_code), --FALTA AKI
    FOREIGN KEY (Airport_code_dep) REFERENCES AIRPORT(Airport_code),
    FOREIGN KEY (Airport_code_arr) REFERENCES AIRPORT(Airport_code),
    FOREIGN KEY (Airplane_id) REFERENCES AIRPLANE(Airplane_id)
);

-- Seat

CREATE TABLE SEAT (
    Seat_no         INT             NOT NULL,
    Customer_name   VARCHAR(50)     NOT NULL,
    Cphone          INT,
    Date            VARCHAR(25)     NOT NULL,
    Leg_no          INT             NOT NULL,
    Flight_Number   INT             NOT NULL,
    PRIMARY KEY (Seat_no,Date,Leg_no,Flight_Number),
    FOREIGN KEY (Date) REFERENCES LEG_INSTANCE(Date),
    FOREIGN KEY (Leg_no) REFERENCES LEG_INSTANCE(Leg_no),
    FOREIGN KEY (Flight_Number) REFERENCES LEG_INSTANCE(Flight_Number)
);