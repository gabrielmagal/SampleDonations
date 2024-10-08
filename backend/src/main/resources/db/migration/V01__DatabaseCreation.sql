create table ReceiveEntity (
    id bigint generated by default as identity,
    dateTimeReceipt timestamp(6),
    name varchar(255),
    quantity integer not null,
    typeOfDonationEnum smallint check (typeOfDonationEnum between 0 and 4),
    validity timestamp(6),
    user_id bigint not null,
    primary key (id)
);

create table SendEntity (
    id bigint generated by default as identity,
    dateTimeDonation timestamp(6),
    quantity integer not null,
    receive_id bigint not null,
    user_id bigint not null,
    primary key (id)
);

create table UserEntity (
    id bigint generated by default as identity,
    cpf varchar(11),
    dateOfBirth date,
    email varchar(255),
    name varchar(255),
    photo varchar(255),
    userPermissionTypeEnum smallint check (userPermissionTypeEnum between 0 and 1),
    primary key (id)
);

alter table if exists ReceiveEntity add constraint FKlv1mylpcx5pd1kjf2toou1c59 foreign key (user_id) references UserEntity;
alter table if exists SendEntity add constraint FK98p6v59y731ygjqeawuubsrjy foreign key (receive_id) references ReceiveEntity;
alter table if exists SendEntity add constraint FK8oig1sixjsad6jkrmt43t9q9o foreign key (user_id) references UserEntity;