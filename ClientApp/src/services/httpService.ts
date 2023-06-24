import AppConsts from '../appconst';
import axios from 'axios';

const http = axios.create({
  baseURL: AppConsts.remoteServiceBaseUrl,
  timeout: 30000,
});

export default http;
