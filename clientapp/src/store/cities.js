import axios from '../helpers/api';
const initState = () => ({
    message: 'cities vuex module',
    cities: [],
    selectedCity: {},
    showInfoModal: false,
    showCreateModal: false,
    showDeleteModal: false
});

export default {
    namespaced: true,
    state: initState,
    actions: {
        fetchCities: async ({commit}) => {
            let response = await axios.get('cities');
            commit('setCities', {cities: response.data});
        },
        deleteCity: async ({dispatch}, {id}) => {
            await axios.delete('cities/'+id);
            dispatch('fetchCities')
        },
        fetchCity: async ({commit}, {name}) => {
            let response = await axios.get('cities?name='+name);
            commit('selectCity', {city: response.data});
        },
        submitForm: async ({dispatch, commit}, {form}) => {
            await axios.post('cities', {
                name: form.name
            });
            commit('setCities', {cities: []});
            dispatch('fetchCities')
        }
    },
    mutations: {
        setCities: (state, {cities}) => {
            state.cities = cities;
        },
        selectCity: (state, {city}) => {
            state.selectedCity = city;
        },
        toggleInfoModal: (state) => {
            state.showInfoModal = !state.showInfoModal;
        },
        toggleCreateModal: (state) => {
            state.showCreateModal = !state.showCreateModal;
        },
        toggleDeleteModal: (state) => {
            state.showDeleteModal = !state.showDeleteModal;
        },


    },
    getters: {
        cities: (state) => {
            return state.cities;
        },
        showInfo: (state) => {
            return state.showInfoModal;
        },
        showCreate: (state) => {
            return state.showCreateModal;
        },
        showDelete: (state) => {
            return state.showDeleteModal;
        },
        selectedCity: (state) => {
            return state.selectedCity;
        }
    }
}
