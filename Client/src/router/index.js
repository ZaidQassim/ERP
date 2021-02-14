import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
    {
        path: "/dashboard",
        name: "أحصائيات النظام",
        component: () => import("../components/Dashboard.vue"),
        meta: {
            requireAuth: true
        }
    },
    {
        path: "/",
        name: "الرئيسية",
        component: () => import("../views/Home.vue"),
        meta: {
            requireAuth: true
        }
    },
    {
        path: "/Login",
        name: "login",
        component: () => import("../views/Login.vue"),
        meta: {
            hideNavigation: true
        }
    },
    {
        path: "*",
        redirect: "/"
    }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
	if (to.matched.some(r => r.meta.requireAuth)) {
		if (localStorage.getItem('token') == undefined || localStorage.getItem('token') == null) {
			next('/login')
		}
	}
	next()
  })

export default router
