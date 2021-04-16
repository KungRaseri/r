<template>
    <v-layout>
        <ChatModule />
    </v-layout>
</template>

<style scoped>
    .home {
        background: none;
    }
</style>

<script lang="ts">
    import ChatModule from '../components/MessageBox/Main.vue';
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            ChatModule
        },
    })
    export default class Home extends Vue { }
</script>
