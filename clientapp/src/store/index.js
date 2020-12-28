import {createStore} from 'vuex'
import axios from "../helpers/api";
import cities from '../store/cities';
import locations from "./locations";

const initState = () => ({
    message: 'hello vuex',
    token: null,
    refreshToken: null,
    role: 'guest',
    loading: false,
    user: {},
    componentKey: 1
});

const store = createStore({
    modules: {cities, locations},
    state: initState,
    actions: {
      authenticate: async ({commit}, {username, password}) => {
        let response = await axios.post('account/authenticate', {username, password});
        commit('setToken', {token: response.data.token});
        commit('setRefreshToken', {refreshToken: response.data.refreshToken});
      },
      userinfo: async ({commit, state}) => {
          if (state.token === null || state.token === 'null'){
              commit('setRole', {role: 'guest'})
          }else{
              let response = await axios.get('account/userinfo');
              let role = response.data.role;
              commit('setRole', {role: role});
              commit('setUser', {user: response.data})
          }
      },
      createAccount: async (context, {username, email, password}) =>{
        await axios.post('account/register', {
            username, email, password
        });
      }
    },
    mutations: {
        setToken: (state, {token}) => {
            state.token = token;
        },
        setRefreshToken: (state, {refreshToken}) => {
            state.refreshToken = refreshToken
        },
        setRole: (state, {role}) => {
            state.role = role;
        },
        setLoading: (state, payload) => {
            state.loading = payload;
        },
        setUser: (state, {user}) => {
            state.user = user;
        },
        initialiseStore: (state) => {
            let token = localStorage.getItem('token');
            let refresh = localStorage.getItem('refreshToken');
            if (token !== false) {
                state.token = token;
            }
            if (refresh !== false) {
                state.refreshToken = refresh;
            }
        },
        resetState: state => {
            state.token = null;
            state.refreshToken = null;
            state.role = 'guest';
        },
        incrementKey: state => {
            state.componentKey++;
        }
    },
    getters: {
        role: state => {
            return state.role;
        },
        user: state => {
            return state.user;
        },
        componentKey: state => {
            return state.componentKey;
        },
        loading: state => {
            return state.loading;
        }
    }
});

store.subscribe((mutation, state) => {
    localStorage.setItem('token', state.token);
    localStorage.setItem('refreshToken', state.refreshToken);
});

export default store;
