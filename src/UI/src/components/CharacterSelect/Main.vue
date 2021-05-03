<template>
    <NewOrExisting />
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import NewOrExisting from './NewOrExisting.vue';

    @Component({
        components: {
            NewOrExisting
        },
    })
    export default class CharacterSelect extends Vue { }
</script>

<style scoped>
</style>