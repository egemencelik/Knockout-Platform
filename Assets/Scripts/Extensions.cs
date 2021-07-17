    using UnityEngine;

    public static class Extensions
    {
        // Extension method to change rigidbody velocity. Use float.NaN to keep the current value.
        public static void SetVelocity(this Rigidbody rb, float x, float y, float z, Vector3 workspace)
        {
            if (float.IsNaN(x)) x = rb.velocity.x;
            if (float.IsNaN(y)) y = rb.velocity.y;
            if (float.IsNaN(z)) z = rb.velocity.z;
            
            workspace.Set(x,y,z);
            rb.velocity = workspace;
        }

        public static void Stop(this Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
        }
        
        public static bool RateLimiter(int frequency)
        {
            return Time.frameCount % frequency == 0;
        }
    }
