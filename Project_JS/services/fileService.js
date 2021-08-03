import { users } from '../app.js';
import fs from 'fs';

export async function overwriteFileAsync() {
    fs.writeFile('./data/users.json', JSON.stringify(users), error => {
        if (error)
            console.log(error);
    });
}