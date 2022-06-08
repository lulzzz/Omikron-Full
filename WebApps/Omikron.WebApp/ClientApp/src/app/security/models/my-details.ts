export interface MyDetails {
    title?: UserTitle;
    firstName : string;
    lastName : string;
    dateOfBirth : Date;
    postcode : string;
    address : string;
    userName : string;
}

export enum UserTitle {
    Mr,
    Mrs,
    Miss,
    Dr,
}
