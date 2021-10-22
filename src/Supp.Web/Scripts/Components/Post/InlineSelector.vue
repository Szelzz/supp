<template>
    <div class="inline-selector">
        <input type="hidden" :name="inputName" :value="selected" />
        <div class="btn-group">

            <button type="button"
                    v-for="(optionName, optionValue) in options"
                    :value="optionValue"
                    class="btn"
                    v-bind:class="{'btn-outline-primary':selected != optionValue ,'btn-primary': selected == optionValue}"
                    @click.prevent="selectOption(optionValue)">
                {{ optionName }}
            </button>
        </div>
    </div>
</template>
<script>
    export default {
        props: {
            options: Object,
            // options looks like this:
            // {
            //   "value1": "name1",
            //   "value2": "name2"
            // }
            inputName: String
        },
        data() {
            return {
                selected: null
            }
        },
        methods: {
            selectOption(option) {
                this.selected = option;
            }
        }
    }
</script>
<style lang="scss">
</style>