package com.example.metaversecodefestadserver.controllers.dto.response;

public class AdResponseDTO {

    private final String sessionId;

    private final String url;

    public AdResponseDTO(String sessionId, String url) {
        this.sessionId = sessionId;
        this.url = url;
    }

    public String getSessionId() {
        return this.sessionId;
    }

    public String getUrl() {
        return this.url;
    }

    public boolean equals(final Object o) {
        if (o == this) return true;
        if (!(o instanceof AdResponseDTO)) return false;
        final AdResponseDTO other = (AdResponseDTO) o;
        if (!other.canEqual((Object) this)) return false;
        final Object this$sessionId = this.getSessionId();
        final Object other$sessionId = other.getSessionId();
        if (this$sessionId == null ? other$sessionId != null : !this$sessionId.equals(other$sessionId)) return false;
        final Object this$url = this.getUrl();
        final Object other$url = other.getUrl();
        if (this$url == null ? other$url != null : !this$url.equals(other$url)) return false;
        return true;
    }

    protected boolean canEqual(final Object other) {
        return other instanceof AdResponseDTO;
    }

    public int hashCode() {
        final int PRIME = 59;
        int result = 1;
        final Object $sessionId = this.getSessionId();
        result = result * PRIME + ($sessionId == null ? 43 : $sessionId.hashCode());
        final Object $url = this.getUrl();
        result = result * PRIME + ($url == null ? 43 : $url.hashCode());
        return result;
    }

    public String toString() {
        return "AdResponseDTO(sessionId=" + this.getSessionId() + ", url=" + this.getUrl() + ")";
    }
}
