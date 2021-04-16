<template>
    <div class="chatbox">
        <MessageBox />
        <v-text-field 
            autofocus="true"
            outlined="true"
            color="black"
        />
    </div>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import MessageBox from './MessageBox.vue';

    @Component({
        components: {
            MessageBox
        },
    })
    export default class ChatModule extends Vue { }
</script>

<style scoped>
    .chatbox {
        margin-left: 1vw;
    }
</style>