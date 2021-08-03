import express from 'express';
import { getBtcRate } from '../controllers/rates.js';
import { auth } from '../middlewares/auth.js';

const router = express.Router();

router.get('/', auth, getBtcRate);

export default router;