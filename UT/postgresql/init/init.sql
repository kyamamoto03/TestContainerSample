--ユーザーの作成
CREATE USER test;
--DBの作成
CREATE DATABASE test;
--ユーザーにDBの権限をまとめて付与
GRANT ALL PRIVILEGES ON DATABASE test TO test;
--ユーザーを切り替え
\c test


--テーブルを作成
create table user_data (
  id varchar(10)
  , name varchar(30) not null
  , constraint user_data_PKC primary key (id)
) ;

comment on table user_data is 'ユーザ';
comment on column user_data.id is 'ユーザID';
comment on column user_data.name is '氏名';

-- 初期データ投入

insert into public.user_data(id,name) values 
    ('001','Testタロウ')
  , ('002','テスト花子');
