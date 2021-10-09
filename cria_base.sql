--
-- PostgreSQL database dump
--

-- Dumped from database version 12.0
-- Dumped by pg_dump version 12.0

-- Started on 2021-10-03 20:37:12 -03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 16579)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 3199 (class 0 OID 0)
-- Dependencies: 7
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 202 (class 1259 OID 16580)
-- Name: ClientGroups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ClientGroups" (
    id integer NOT NULL,
    client_id integer,
    group_id integer,
    attendance integer DEFAULT 0,
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."ClientGroups" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16584)
-- Name: ClientGroups_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."ClientGroups_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."ClientGroups_id_seq" OWNER TO postgres;

--
-- TOC entry 3200 (class 0 OID 0)
-- Dependencies: 203
-- Name: ClientGroups_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."ClientGroups_id_seq" OWNED BY public."ClientGroups".id;


--
-- TOC entry 204 (class 1259 OID 16586)
-- Name: Clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Clients" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    cpf character varying(11),
    email character varying(100),
    phone character varying(20),
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL,
    teacher boolean DEFAULT false,
    cel_phone character varying(20),
    com_phone character varying(20),
    address1 character varying(255),
    address2 character varying(255),
    address3 character varying(255),
    city character varying(255),
    state character varying(255),
    zip_code character varying(8),
    profession character varying(30),
    edu_lvl character varying(2),
    old_code character varying(10),
    birth_date date,
    birth_hour time without time zone,
    birth_place character varying(50),
    note text
);


ALTER TABLE public."Clients" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 16593)
-- Name: Clients_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Clients_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Clients_id_seq" OWNER TO postgres;

--
-- TOC entry 3201 (class 0 OID 0)
-- Dependencies: 205
-- Name: Clients_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Clients_id_seq" OWNED BY public."Clients".id;


--
-- TOC entry 206 (class 1259 OID 16595)
-- Name: Courses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Courses" (
    id integer NOT NULL,
    code character varying(255),
    name character varying(255) NOT NULL,
    description character varying(255),
    "courseLoad" integer,
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Courses" OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16601)
-- Name: Courses_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Courses_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Courses_id_seq" OWNER TO postgres;

--
-- TOC entry 3202 (class 0 OID 0)
-- Dependencies: 207
-- Name: Courses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Courses_id_seq" OWNED BY public."Courses".id;


--
-- TOC entry 208 (class 1259 OID 16603)
-- Name: Groups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Groups" (
    id integer NOT NULL,
    course_id integer,
    start_date date NOT NULL,
    end_date date NOT NULL,
    start_hour time without time zone,
    end_hour time without time zone,
    class_info json,
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL,
    teacher_id integer,
    classes integer
);


ALTER TABLE public."Groups" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16609)
-- Name: Groups_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Groups_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Groups_id_seq" OWNER TO postgres;

--
-- TOC entry 3203 (class 0 OID 0)
-- Dependencies: 209
-- Name: Groups_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Groups_id_seq" OWNED BY public."Groups".id;


--
-- TOC entry 210 (class 1259 OID 16611)
-- Name: Payments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Payments" (
    "clientGroup_id" integer NOT NULL,
    value numeric(10,2) NOT NULL,
    paid boolean DEFAULT false,
    due_date date,
    installment integer DEFAULT 1 NOT NULL,
    note text,
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL,
    number_installments integer
);


ALTER TABLE public."Payments" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16619)
-- Name: SequelizeMeta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SequelizeMeta" (
    name character varying(255) NOT NULL
);


ALTER TABLE public."SequelizeMeta" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 16622)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    id integer NOT NULL,
    username character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    email character varying(255),
    "createdAt" timestamp with time zone NOT NULL,
    "updatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16628)
-- Name: Users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Users_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Users_id_seq" OWNER TO postgres;

--
-- TOC entry 3204 (class 0 OID 0)
-- Dependencies: 213
-- Name: Users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Users_id_seq" OWNED BY public."Users".id;


--
-- TOC entry 3044 (class 2604 OID 31149)
-- Name: ClientGroups id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ClientGroups" ALTER COLUMN id SET DEFAULT nextval('public."ClientGroups_id_seq"'::regclass);


--
-- TOC entry 3046 (class 2604 OID 31150)
-- Name: Clients id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Clients" ALTER COLUMN id SET DEFAULT nextval('public."Clients_id_seq"'::regclass);


--
-- TOC entry 3047 (class 2604 OID 31151)
-- Name: Courses id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Courses" ALTER COLUMN id SET DEFAULT nextval('public."Courses_id_seq"'::regclass);


--
-- TOC entry 3048 (class 2604 OID 31152)
-- Name: Groups id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Groups" ALTER COLUMN id SET DEFAULT nextval('public."Groups_id_seq"'::regclass);


--
-- TOC entry 3051 (class 2604 OID 31153)
-- Name: Users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users" ALTER COLUMN id SET DEFAULT nextval('public."Users_id_seq"'::regclass);


--
-- TOC entry 3053 (class 2606 OID 16636)
-- Name: ClientGroups ClientGroups_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ClientGroups"
    ADD CONSTRAINT "ClientGroups_pkey" PRIMARY KEY (id);


--
-- TOC entry 3055 (class 2606 OID 16638)
-- Name: Clients Clients_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Clients"
    ADD CONSTRAINT "Clients_pkey" PRIMARY KEY (id);


--
-- TOC entry 3057 (class 2606 OID 16640)
-- Name: Courses Courses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Courses"
    ADD CONSTRAINT "Courses_pkey" PRIMARY KEY (id);


--
-- TOC entry 3059 (class 2606 OID 16642)
-- Name: Groups Groups_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Groups"
    ADD CONSTRAINT "Groups_pkey" PRIMARY KEY (id);


--
-- TOC entry 3061 (class 2606 OID 16644)
-- Name: Payments Payments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "Payments_pkey" PRIMARY KEY ("clientGroup_id", installment);


--
-- TOC entry 3063 (class 2606 OID 16646)
-- Name: SequelizeMeta SequelizeMeta_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SequelizeMeta"
    ADD CONSTRAINT "SequelizeMeta_pkey" PRIMARY KEY (name);


--
-- TOC entry 3065 (class 2606 OID 16648)
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY (id);


--
-- TOC entry 3066 (class 2606 OID 16649)
-- Name: Groups Groups_course_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Groups"
    ADD CONSTRAINT "Groups_course_id_fkey" FOREIGN KEY (course_id) REFERENCES public."Courses"(id);


--
-- TOC entry 3067 (class 2606 OID 16654)
-- Name: Payments fkey_group_client; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT fkey_group_client FOREIGN KEY ("clientGroup_id") REFERENCES public."ClientGroups"(id);


-- Completed on 2021-10-03 20:37:12 -03

--
-- PostgreSQL database dump complete
--

