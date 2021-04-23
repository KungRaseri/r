import Vue, { PluginObject } from 'vue';
import axios from 'axios';

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

const config = {
    // baseURL: process.env.baseURL || process.env.apiUrl || ""
    // timeout: 60 * 1000, // Timeout
    // withCredentials: true, // Check cross-site Access-Control
};

const _axios = axios.create(config);

_axios.interceptors.request.use(
    (cfg: any) => {
        // Do something before request is sent
        return cfg;
    },
    (err: any) => {
        // Do something with request error
        return Promise.reject(err);
    }
);

// Add a response interceptor
_axios.interceptors.response.use(
    (res: any) => {
        // Do something with response data
        return res;
    },
    (err: any) => {
        // Do something with response error
        return Promise.reject(err);
    }
);

const AxiosPlugin: PluginObject<any> = {
    install: (VueApp: any) => {
        VueApp.$axios = _axios;
    },
};
AxiosPlugin.install = (VueApp: any) => {
    VueApp.$axios = _axios;
    (window as any).axios = _axios;
    Object.defineProperties(VueApp.prototype, {
        $axios: {
            get() {
                return _axios;
            },
        },
    });
};

Vue.use(AxiosPlugin);

export default AxiosPlugin;
