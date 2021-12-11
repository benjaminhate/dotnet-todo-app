const api = new API()

const todoItemVue = new Vue({
    el: '#todo-item',
    data: {
        item: {}
    },
    methods: {
        goBackHome(){
            window.location = document.location.origin
        },
        getItemId(){
            const urlParams = new URLSearchParams(window.location.search);
            return urlParams.get('id');
        }
    },
    beforeMount: async function(){
        let id = this.getItemId()
        if(id === null)
            this.goBackHome()
        let item = await api.getTodo(id)
        if(item === null)
            this.goBackHome()
        this.item = item
    }
})