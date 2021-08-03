import * as EmailValidator from 'email-validator';
import bcrypt from 'bcryptjs';
import jwt from 'jsonwebtoken';
import { users } from '../app.js'
import { overwriteFileAsync } from '../services/fileService.js';

export const createUser = async (request, response) => {
    const user = request.body;
    if (!EmailValidator.validate(user.email))
        return response.status(400).send("Email is not validated!");

    if (user.password.length < 6)
        return response.status(400).send("Password is not validated! Min. length should be 6!")

    const wantedUser = users.find(u => u.email == user.email);
    if (wantedUser != undefined)
        return response.status(400).send("User with this email alredy exists!");

    const salt = await bcrypt.genSalt(10);
    user.password = await bcrypt.hash(user.password, salt);

    users.push(user);
    await overwriteFileAsync();

    return response.send("User is successfully created!");
}

export const loginUser = async (request, response) => {
    const user = request.body;
    if (!EmailValidator.validate(user.email))
        return response.status(400).send("Email is not validated!");

    if (user.password.length < 6)
        return response.status(400).send("Password is not validated! Min. length should be 6!");

    const wantedUser = users.find(u => u.email == user.email);
    if (wantedUser == undefined)
        return response.status(400).send("Email or password is not correct!")

    const isPasswordCorrect = await bcrypt.compare(user.password, wantedUser.password);
    if (!isPasswordCorrect)
        return response.status(400).send("Email or password is not correct!");

    const token = jwt.sign({ email: wantedUser.email }, process.env.secret);
    return response.header('auth-token', token).send(token);
}