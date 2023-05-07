import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import User from "src/app/models/user.model";


const UserController = "http://localhost:5228/Users/";
@Injectable()
class UserService {
    constructor(private httpClient: HttpClient) {

    }

    get(): Observable<User[]> {
        return this.httpClient.get<User[]>(UserController);
    }
    getAllUsersByLoggedIn(user: User): Observable<User[]> {
        return this.httpClient.post<User[]>(UserController + "UsersByLoggedIn", user);
    }

    getById(id: number): Observable<User> {
        return this.httpClient.get<User>(UserController + id);
    }

    register(user: User): Observable<boolean> {
        return this.httpClient.post<boolean>(UserController + 'register', user);
    }

    login(user: User): Observable<User> {
        return this.httpClient.post<User>(UserController + 'login', user);
    }

    resetStatus(user: User): Observable<User> {
        return this.httpClient.put<User>(UserController + 'reset', user);
    }

    getToken(user: User): Observable<User> {
        return this.httpClient.post<User>(UserController + 'SendToken', user);
    }
}
export default UserService;