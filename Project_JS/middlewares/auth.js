import jwt from 'jsonwebtoken';

export function auth(request, response, next) {
    const token = request.header('auth-token');
    if (!token)
        return response.status(401).send('Access denied!');

    try {
        const hasAccess = jwt.verify(token, process.env.secret);
        request.user = hasAccess;
        next();
    } catch (error) {
        return response.status(401).send('Ivalid token!');
    }
}