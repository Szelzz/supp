<template>
    <span>
        <!--Priority-->
        <i v-if="priority == 'Unset'" title="Nieprzydzielony" class="far fa-square"></i>
        <i v-if="priority == 'Unimportant'" title="Mało ważny" class="fas fa-arrow-down"></i>
        <i v-if="priority == 'Normal'" title="Normalny" class="fas fa-check"></i>
        <i v-if="priority == 'Important'" title="Ważny" class="fas fa-clipboard-exclamation"></i>

        <!--Type-->
        <i v-if="type == 'Error'" title="Błąd" class="fas fa-bug"></i>
        <i v-if="type == 'Enhancement'" title="Ulepszenie" class="fas fa-angle-double-up"></i>
        <i v-if="type == 'Task'" title="Zadanie" class="fas fa-clipboard-check"></i>
    </span>
</template>
<script>
    export default {
        props: {
            type: String,
            priority: String,
        },
        data() {
            return {
            }
        }
    }
</script>