<template>
    <v-layout>
        <MessageBox />
    </v-layout>
</template>

<style scoped>
    .home {
        background: none;
    }
</style>

<script lang="ts">
    import MessageBox from '../components/MessageBox/Main.vue';
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            MessageBox
        },
    })
    export default class Home extends Vue { }
</script>
