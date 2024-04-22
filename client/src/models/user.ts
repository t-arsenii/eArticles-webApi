export interface IUser {
    id: string,
    firstName: string
    lastName: string
    userName: string
    email: string
    phoneNumber: string
}
export interface IUserRegReq {
    firstName: string
    lastName: string
    userName: string
    email: string
    phoneNumber: string,
    password: string
}
export interface IUserRegRes {
    id:string,
    firstName: string
    lastName: string
    userName: string
    email: string
    phoneNumber: string,
    password: string
}
export interface IUserAuthReq {
    userName : string,
    password: string 
}
export interface IUserAuthRes {
    token : string,
    expiration: string 
}

export interface IUserState {
    token: string | null;
    userInfo: IUser;
  }