<template>
    <button @click="click" class="float-right">
        <i class="fas fa-pen-square"></i>
    </button>
</template>

<script>

    export default {
        props: ['target'],
        data() {
            return {

            };
        },
        methods: {
            click() {
                alert(this.target);
            }
        }
    }
</script>

<style lang="scss" scoped>
</style>