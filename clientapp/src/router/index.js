import { createRouter, createWebHistory } from 'vue-router'
import store from "../store/index";

import Home from '../views/Home.vue'
import Register from "../views/Register";
import Locations from "../views/panel/Locations";
import Cities from "../views/panel/Cities";
import Login from "../views/Login";
import UserInfo from "../views/panel/UserInfo";
import SingleLocation from "../views/panel/SingleLocation";
import About from "../views/About"

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    children: [
      {path: '', name: '', component: About},
      {path: 'locations', name: 'Locations', component: Locations, meta: { authorize: 'user' }},

      {path: 'locations/:id', name: 'SingleLocation', component: SingleLocation,  meta: { authorize: 'user' }},

      {path: 'cities', name: 'Cities', component: Cities,  meta: { authorize: 'user' }},
      {path: 'userinfo', name: 'UserInfo', component: UserInfo,  meta: { authorize: 'user' }},
    ]
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
    meta: { authorize: '' },
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  const { authorize } = to.meta;
  const token = store.state.token;
  const role = store.state.role;
  if (authorize) {
    if (token === null || token === 'null') {
      return next({ path: '/login'});
    }

    if (authorize !== role) {
      console.log(authorize, role);
      return next({ path: '/' });
    }
  }
  next();
});

export default router
