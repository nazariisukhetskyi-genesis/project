import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import dotenv from 'dotenv';
import userRoutes from './routes/users.js';
import btcRoute from './routes/rates.js';

export let users = JSON.parse(fs.readFileSync('./data/users.json', 'utf-8'));

dotenv.config();

const app = express();
const PORT = 5000;

app.use(bodyParser.json());
app.use('/', userRoutes);
app.use('/btcRate', btcRoute);

app.get('/', (request, response) => {
    response.send('This project is made by Nazarii Sukhetskyi as additional for the genesis practical task!');
});

app.listen(PORT, () => console.log(`Server running on port: http://localhost:${PORT}`));