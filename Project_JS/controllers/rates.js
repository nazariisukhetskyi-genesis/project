import https from 'https';

const options = {
    "method": "GET",
    "hostname": "rest.coinapi.io",
    "path": "/v1/exchangerate/BTC/UAH",
    "headers": { 'X-CoinAPI-Key': '48B2ABDF-D42F-476D-9E92-7CF692778DD6' }
};

export const getBtcRate = async (request, response) => {
    https.get(options, res => {
        var chunks = '';
        res.on('data', chunk => {
            chunks += chunk;
        });

        res.on('end', () => {
            return response.send({
                rate: JSON.parse(chunks).rate
            });
        });

        res.on('error', error => {
            return response.status(500).send(error.message);
        });
    });
}