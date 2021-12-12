const api = new API()

const todoListVue = new Vue({
    el: '#todo-list',
    data: {
        list: [],
        refreshing: false
    },
    methods: {
        async refresh(){
            this.refreshing = true
            let newList = await api.getAllTodos()
            if(newList !== null)
                this.list = newList
            this.refreshing = false;
        },
        async complete(item){
            item.isComplete = !item.isComplete
            await api.modifyTodo(item)
            if(item.isComplete)
                this.pushItemToLast(item)
        },
        pushItemToLast(item){
            this.list.push(this.list.splice(this.list.indexOf(item), 1)[0])
        },
        goToDetail(item){
            window.location = document.location.origin + "/todo.html?id="+item.id
        }
    }
})

todoListVue.refresh()