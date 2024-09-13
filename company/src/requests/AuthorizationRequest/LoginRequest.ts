export class LoginRequest {
    constructor(identificator = "", password = "") {
        this.identificator = identificator;
        this.password = password;
    }
    identificator: string;
    password: string;
}