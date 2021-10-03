<script>
    import PostIcon from './PostIcon.vue'

    export default {
        components: {
            PostIcon
        },
        props: {
            searchUrl: String,
            projectId: Number,
        },
        data() {
            return {
                posts: []
            }
        },
        created() {
            this.$root.$on('search-success', p => this.posts = p);
        },
        methods: {
        }
    }
</script>