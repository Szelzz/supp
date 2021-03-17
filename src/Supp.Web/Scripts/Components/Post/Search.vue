<script>
    import Ajax from '../../Ajax'

    export default {
        props: {
            searchUrl: String,
            projectId: Number,
        },
        data() {
            return {
                text: "",
                statuses: [1, 2, 3, 4, 5],
                types: [0, 1, 2],
                priorities: [0, 1, 2, 3]
            }
        },
        created() {
            this.search();
        },
        methods: {
            search() {
                Ajax.apiRequest(this.searchUrl, this.prepareSearchQuery(), this.success, this.error)
            },
            prepareSearchQuery() {
                return {
                    projectId: this.projectId,
                    text: this.text,
                    statuses: this.statuses,
                    types: this.types,
                    priorities: this.priorities
                }
            },
            addComment() {
            },
            success(response) {
                this.$root.$emit("search-success", response.data);
            },
            error(response) {

            }
        }
    }
</script>